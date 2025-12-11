namespace Data.Seed;

internal interface ISeeder
{
    Task SeedAsync(AppDbContext context, IServiceProvider services);
}