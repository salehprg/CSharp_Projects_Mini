using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace Backend.Models
{
    public partial class SmsPanelDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
             if (!optionsBuilder.IsConfigured)
            {
                IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);


                IConfigurationRoot configurationRoot = configurationBuilder.Build();
                configurationRoot.GetConnectionString("Default");
                optionsBuilder.UseNpgsql(configurationRoot.GetConnectionString("SmsPanel"));
            }
        }

    }
}
