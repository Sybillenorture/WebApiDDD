using Lamar;
using Lamar.Microsoft.DependencyInjection;
using Lucca.ExpenseApp.Api;
using Lucca.ExpenseApp.Domain.Entities;
using Lucca.ExpenseApp.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LuccaExpenses.Tests
{
    public class BaseTests
    {
        private static readonly Lazy<IContainer> _container = new Lazy<IContainer>(InitContainer);

        public static IContainer Container => _container.Value;

        private static IContainer InitContainer()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "UnitTest");

            var hostBuilder = Host.CreateDefaultBuilder()
                .UseLamar()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .UseEnvironment("UnitTest");
                });

            var host = hostBuilder.Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ExpenseAppDbContext>();
                context.Database.EnsureCreated();
                //SeedDatabase(context);  // Seed database with initial data

            }

            IContainer container = (IContainer)host.Services;
            container.AssertConfigurationIsValid();

            return container;
        }

        private static void SeedDatabase(ExpenseAppDbContext dbContext)
        {
            if (!dbContext.Claimants.Any())
            {
                dbContext.Claimants.AddRange(
                    new Claimant { Id = 1, FirstName = "Anthony", LastName = "Stark", Currency = "USD" },
                    new Claimant { Id = 2, FirstName = "Natasha", LastName = "Romanova", Currency = "RUB" }
                );
                dbContext.SaveChanges();
            }
        }
    }
}
