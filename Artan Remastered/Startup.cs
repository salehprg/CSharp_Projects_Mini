using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using artan_gym.Models;
using Models.User;
using artan_gym.Helper;
using System.Text;
using System;

namespace artan_gym
{
    public class Startup
    {
        public Startup(IConfiguration configuration , IWebHostEnvironment _env)
        {
            Configuration = configuration;
            environment = _env;
        }

        public readonly string AllowOrigin = "AllowOrigin";
        public readonly IWebHostEnvironment environment;
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy(
                AllowOrigin , builder => {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                        
                }
            ));

            string conStr = "" ; 

            if(!environment.IsDevelopment())
            {
                // IConfigurationSection section = Configuration.GetSection("AppSettings");

                // section.Get<AppSettings>();
                
                // conStr = Configuration.GetConnectionString("PublishConnection_PS");

                string host = Environment.GetEnvironmentVariable("GYM_DATABASE_HOST");
                string name = Environment.GetEnvironmentVariable("GYM_DATABASE_NAME");
                string userName = Environment.GetEnvironmentVariable("GYM_DATABASE_USER");
                string password = Environment.GetEnvironmentVariable("GYM_DATABASE_PASSWORD");

                conStr = string.Format("Server={0};Database={1};Username={2};Password={3}" , host , name , userName ,password);
                
                AppSettings.Secret = Environment.GetEnvironmentVariable("GYM_JWT_SECRET");

            }
            else
            {
                IConfigurationSection section = Configuration.GetSection("AppSettings");

                section.Get<AppSettings>();
                
                conStr = Configuration.GetConnectionString("LegaceConnection");
            }

            services.AddDbContext<ApplicationDbContext>(options => {
                    options.UseNpgsql(conStr);
                });

            services.AddIdentity<UserModel , IdentityRole<int>>(
                options => 
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireDigit = false;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            var key = Encoding.ASCII.GetBytes(AppSettings.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidAudience = "http://lms.legace.ir",
                    ValidIssuer = "http://lms.legace.ir"
                };
            });

            services.AddControllersWithViews();
            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(AllowOrigin);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseRouting();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
        
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";
                
                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
