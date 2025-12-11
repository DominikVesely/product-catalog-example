namespace Data.Seed;

internal class DbSeeder
{
    private readonly IEnumerable<ISeeder> _seeders;
    public DbSeeder(IEnumerable<ISeeder> seeders) => _seeders = seeders;

    public async Task SeedAllAsync(AppDbContext context, IServiceProvider services)
    {
        foreach (var seeder in _seeders)
        {
            await seeder.SeedAsync(context, services);
        }
    }
}