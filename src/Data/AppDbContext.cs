using Data.Configuration.Entities;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data;

internal sealed class AppDbContext : DbContext
{
    public const string OptionsName = nameof(AppDbContext);

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
    }
}