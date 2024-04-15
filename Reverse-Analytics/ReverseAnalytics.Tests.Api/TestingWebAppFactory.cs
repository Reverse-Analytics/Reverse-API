using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Tests.Api;

public class TestingWebAppFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite("Data Source = ApiTestsDB",
                    x => x.MigrationsAssembly("ReverseAnalytics.Migrations.Sqlite"));
            });

            var serviceProvider = services.BuildServiceProvider();

            using var scope = serviceProvider.CreateScope();
            using var appContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            try
            {
                // appContext.Database.EnsureDeleted();
                appContext.Database.EnsureCreated();
            }
            catch (Exception)
            {
                throw;
            }
        });
    }
}