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
        // database
        services.AddDbContext<AppDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            options.UseSqlServer(connectionString, x => x.MigrationsAssembly("Data"));
        });

        // repositories
        services.AddScoped<IProductRepository, ProductRepository>();

        // seed
        services.AddScoped<ISeeder, ProductSeedData>();
        services.AddScoped<DbSeeder>();

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
}
