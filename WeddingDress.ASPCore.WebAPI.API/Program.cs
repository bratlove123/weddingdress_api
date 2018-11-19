using System;
using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Ef;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Models.Entities;

namespace WeddingDress.ASPCore.WebAPI.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<WeddingDressDataContext>();
                try
                {
                    Initialize(services);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

        private static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<WeddingDressDataContext>();
            context.Database.EnsureCreated();
            if (!context.Products.Any())
            {
                context.Products.Add(new Product() { Name = "Hello" });
                context.Products.Add(new Product() { Name = "Sample" });
                context.SaveChanges();
            }

            //if (!context.LeftNavItems.Any())
            //{
            //    context.LeftNavItems.Add(new LeftNavItem() { Name="By Date", Url="#" });
            //}

            //if (!context.LeftNavs.Any())
            //{
            //    context.LeftNavs.Add(new LeftNav() { Name = "Dashboard", Url="#", IsHasBadge=false });
            //    context.SaveChanges();
            //}
        }
    }
}
