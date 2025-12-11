using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

internal sealed class ProductRepository : IProductRepository
{
    private readonly AppDbContext _db;

    public ProductRepository(AppDbContext db) => _db = db;

    public async Task<List<Product>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _db.Products.ToListAsync(cancellationToken);
    }

    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _db.Products.FindAsync(new object?[] { id }, cancellationToken);
    }

    public async Task UpdateAsync(Product product, CancellationToken cancellationToken = default)
    {
        _db.Products.Update(product);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
