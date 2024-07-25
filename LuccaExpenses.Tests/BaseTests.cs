
using Lamar;
using Lamar.Microsoft.DependencyInjection;
using LuccaExpenses.Api;
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
                var context = services.GetRequiredService<LuccaExpensesDbContext>();
                context.Database.EnsureCreated(); 
            }

            IContainer container = (IContainer)host.Services;
            container.AssertConfigurationIsValid();

            return container;
        }
    }
}
