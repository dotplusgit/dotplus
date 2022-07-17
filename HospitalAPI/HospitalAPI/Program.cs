using HospitalAPI.Core.Models;
using HospitalAPI.DataAccess.Data;
using HospitalAPI.DataAccess.Data.SeedData;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace HospitalAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var service = scope.ServiceProvider;
                var loggerFactory = service.GetRequiredService<ILoggerFactory>();
                try
                {
                    var dbContext = service.GetRequiredService<ApplicationDbContext>();
                    await DbContextSeed.SeedHospitalAsync(dbContext);
                    await DbContextSeed.SeedFollowupAsync(dbContext);
                    await DbContextSeed.SeedPhysicalstatToPrescriptionAsync(dbContext);
                    var userManager = service.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = service.GetRequiredService<RoleManager<ApplicationRole>>();
                    await DbContextSeed.SeedRolesAsync(userManager, roleManager);
                    await DbContextSeed.SeedUsersAsync(userManager);
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex, "An error occured during seed");
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
