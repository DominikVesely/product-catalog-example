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

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Price).HasColumnType("money");
        });
    }
}