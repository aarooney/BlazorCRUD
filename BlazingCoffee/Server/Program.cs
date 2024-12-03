using BlazingCoffee.Server.Data;
using BlazingCoffee.Shared.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace BlazingCoffee.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            CreateDbIfNotExists(host);
















            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<CoffeeContext>();
                    var environment = services.GetRequiredService<IWebHostEnvironment>();
                    context.Database.Migrate();

                    var newEmployee = new Employee
                    {
                        TeamId = 4,
                        FullName = "Big Fudge",  // Changed name to Cornelius Fudge
                        JobTitle = "Marketing Manager",
                        Country = "FR",
                        IsOnline = false,
                        Rating = 3,
                        Target = 68,
                        Budget = 33697,
                        Phone = "(539) 9486139",
                        Address = "Gringotts",
                        ImgId = 1,
                        Gender = "M"
                    };

                    context.Employees.Add(newEmployee);
                    context.SaveChanges();






                    DbInitializer.Initialize(context, environment);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }
    }
}
