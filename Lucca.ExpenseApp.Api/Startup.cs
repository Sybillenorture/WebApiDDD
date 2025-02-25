﻿using Lamar;
using Lucca.ExpenseApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Lucca.ExpenseApp.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
         
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "UnitTest")
            {
                services.AddDbContext<ExpenseAppDbContext>(options =>
                    options.UseInMemoryDatabase("InMemoryDbForTesting"));
            }
            else
            {
                services.AddDbContext<ExpenseAppDbContext>(options =>
                    options.UseNpgsql(Configuration.GetConnectionString("LuccaExpensesDbContext") ?? throw new InvalidOperationException("Connection string 'LuccaExpensesDbContext' not found.")));
            }

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddLogging();
            services.AddMvc();
            services.AddControllers();
            
            // generate responses detail problem for all client / serveur Http response taht doesn't have a body yet
            services.AddProblemDetails();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void ConfigureContainer(ServiceRegistry services)
        {
            services.IncludeRegistry<IoCRegistry>();
            services.Scan(s =>
            {
                s.TheCallingAssembly();
                s.WithDefaultConventions();
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configurer le pipeline de traitement des requêtes HTTP
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();

            app.UseEndpoints(endpoint =>
            {
                endpoint.MapControllers();
            });

            // Apply migrations and seed database
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ExpenseAppDbContext>();
                dbContext.Database.EnsureCreated();
            }

            var container = app.ApplicationServices.GetService<IContainer>();
            container?.AssertConfigurationIsValid();
        }
    }
}
