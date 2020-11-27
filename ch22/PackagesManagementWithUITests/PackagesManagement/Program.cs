using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PackagesManagementDB;
using PackagesManagementDB.Extensions;
using System;
using System.Threading.Tasks;

namespace PackagesManagement
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args)
                .Build();

            await SeedDatabase(host.Services);
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static async Task SeedDatabase(IServiceProvider serviceProvider)
        {
            using var serviceScope = serviceProvider.CreateScope();
            var context = serviceScope.ServiceProvider.GetRequiredService<MainDbContext>();

            await context.Seed(serviceScope);
        }
    }
}
