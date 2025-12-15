using Data.Repositories;
using Data.Seed;
using Data.Seed.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Configuration;

public static class DataConfiguration
{
    public static IServiceCollection AddDataLayer(this IServiceCollection services, IConfiguration configuration)
    {
        var mockDataOptions = configuration.GetSection(MockDataOptions.SectionName)
            .Get<MockDataOptions>() ?? new MockDataOptions();

        // repositories
        if (mockDataOptions.Enabled)
        {
            services.AddScoped<IProductRepository, ProductRepositoryJson>();
        }
        else
        {
            RegisterDbContext(services, configuration);
            services.AddScoped<IProductRepository, ProductRepositoryEF>();
        }

        // seeds
        services.AddScoped<DbSeeder>();
        services.AddScoped<ISeeder, ProductSeedData>();

        return services;
    }

    public static async Task ApplySeedDataAsync(this IServiceProvider provider)
    {
        using (var scope = provider.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            db.Database.Migrate();

            var seeder = scope.ServiceProvider.GetRequiredService<DbSeeder>();
            await seeder.SeedAllAsync(db, scope.ServiceProvider);
        }
    }

    private static void RegisterDbContext(IServiceCollection services, IConfiguration configuration)
    {
        // database
        services.AddDbContext<AppDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            options.UseSqlServer(connectionString, x => x.MigrationsAssembly("Data"));
        });
    }

}
