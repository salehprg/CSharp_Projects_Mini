using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Helpers;
using Backend.Helpers.Permissionfolder;
using Backend.Jobs;
using Backend.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace Backend {
    public class Startup {
        readonly string MyAllowSpecificOrigins = "AllowOrigin";
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            var appBankSection = Configuration.GetSection ("AppBankInfo");
            services.Configure<AppBankInfo> (appBankSection);

            var appPathSection = Configuration.GetSection ("AppPath");
            services.Configure<AppPath> (appPathSection);

            var appAuthSection = Configuration.GetSection ("AppAuth");
            services.Configure<AppAuth> (appAuthSection);
            var appAuth = appAuthSection.Get<AppAuth> ();

            var key = Encoding.UTF8.GetBytes (appAuth.Secret);
            
            services.AddAuthentication (op => {
                op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;


            }).AddJwtBearer (op => {
                op.RequireHttpsMetadata = false;
                op.SaveToken = false;
                op.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey (key)
                };
            });
            //c.AddPolicy(MyAllowSpecificOrigins, options => options.WithOrigins("http://localhost:4200").AllowAnyHeader()
            //                         .AllowAnyMethod());
            // services.AddCors(c =>
            //       {
            //           c.AddPolicy(MyAllowSpecificOrigins, options => options.AllowAnyOrigin().AllowAnyHeader()
            //                           .AllowAnyMethod());
            //       });

            services.AddRouting (r => r.SuppressCheckForUnhandledSecurityMetadata = true);
            services.AddCors (o => o.AddPolicy (MyAllowSpecificOrigins, builder => {
                builder.AllowAnyOrigin ()
                    .AllowAnyMethod ()
                    .AllowAnyHeader ();
            }));

            services.AddDbContext<SmsPanelDbContext> (
                op => op.UseNpgsql (Configuration.GetConnectionString ("PayanakDB"))
                // ServiceLifetime.Singleton
            );
            #region Quartz

            services.AddSingleton<IJobFactory, SingletonJobFactory> ();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory> ();

            services.AddSingleton<BusinessCardJob> ();
            services.AddSingleton (new JobSchedule (jobType: typeof (BusinessCardJob), cronExpression:
                "0/5 * * * * ?"
            ));
            // 0 0 0 * * ?
            services.AddSingleton<ScheduleSmsJob> ();
            services.AddSingleton (new JobSchedule (jobType: typeof (ScheduleSmsJob), cronExpression:
                "0/5 * * * * ?"
            ));

            services.AddSingleton<TasksJob> ();
            services.AddSingleton (new JobSchedule (jobType: typeof (TasksJob), cronExpression:
                "0/5 * * * * ?"
            ));

            services.AddHostedService<QuartzHostedService> ();
            services.AddSpaStaticFiles (configuration => {
                configuration.RootPath = "ClientApp/dist";
            });
            #endregion

            services.AddAuthorization();

            services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();

            services.AddControllers ();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }

            // We only use kerstrel in HTTP mode
            // app.UseHttpsRedirection();

            // app.UseCors();
            app.UseCors (MyAllowSpecificOrigins);

            app.UseRouting ();

            app.UseAuthentication ();

            app.UseAuthorization ();

            var ImagePath = Path.Combine (Directory.GetCurrentDirectory (), "Images");
            var FilesPath = Path.Combine (Directory.GetCurrentDirectory (), "Files");

            if (!Directory.Exists (ImagePath)) {
                Directory.CreateDirectory (ImagePath);
            }
            if (!Directory.Exists (FilesPath)) {
                Directory.CreateDirectory (FilesPath);
            }
            app.UseStaticFiles (new StaticFileOptions {

                FileProvider = new PhysicalFileProvider (ImagePath),
                    RequestPath = "/images"
            });
            app.UseStaticFiles (new StaticFileOptions {
                FileProvider = new PhysicalFileProvider (FilesPath),
                    RequestPath = "/files"
            });

            app.Use (async (context, next) => {
                await next ();
                if (context.Response.StatusCode == 404 && !context.Request.Path.Value.StartsWith ("/api/")) {
                    context.Request.Path = "/index.html";
                    await next ();
                }
            });

            app.UseStaticFiles ();
            app.UseSpaStaticFiles ();
            app.UseDefaultFiles ();
            app.UseEndpoints (endpoints => {
                endpoints.MapControllers ();
            });
            app.UseSpa (spa => {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment ()) {
                    spa.UseAngularCliServer (npmScript: "start");
                }
            });
        }
    }
}