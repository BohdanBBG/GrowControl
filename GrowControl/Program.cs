using GrowControl.Models.Domain;
using GrowControl.DbContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.Extensions.FileProviders;

namespace GrowControl
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var webBuilder = WebApplication.CreateBuilder(args);
            var appConfig = GetAppConfig();

            AddServices(webBuilder);
            AddDefaultData(appConfig);

            var app = webBuilder.Build();

            ConfigurePipline(app, appConfig);

            app.Run();
        }

        public static IConfigurationRoot GetAppConfig()
        {
            var configurationBuilder = new ConfigurationBuilder();

            configurationBuilder.SetBasePath(Directory.GetCurrentDirectory());
            configurationBuilder.AddJsonFile("appsettings.json");

            return configurationBuilder.Build();
        }

        public static void AddDefaultData(IConfigurationRoot appConfig)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MainContext>();
            var options = optionsBuilder
                    .UseSqlServer(appConfig.GetConnectionString("DefaultConnection"))
                    .Options;

            using (MainContext db = new MainContext(options))
            {
                var plants = db.Plants.ToList();

                foreach (Plant plant in plants)
                {
                    Console.WriteLine($"{plant.Id}.{plant.Name} - {plant.Planted} - {plant.GrowthTime}");
                }
            }
        }

        public static void AddServices(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();
        }

        public static void ConfigurePipline(WebApplication app, IConfigurationRoot appConfig)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger(options =>
                {
                    options.SerializeAsV2 = true;
                });
                app.UseSwaggerUI();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    FileProvider = new PhysicalFileProvider
            //    (
            //        Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\app")
            //    ),
            //});

            app.MapDefaultControllerRoute();
        }
    }
}