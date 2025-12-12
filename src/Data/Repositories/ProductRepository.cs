using Business.Extensions;
using Data.Dto;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

internal sealed class ProductRepository : IProductRepository
{
    private readonly AppDbContext _db;

    public ProductRepository(AppDbContext db) => _db = db ?? throw new ArgumentNullException(nameof(db));

    public Task<List<Product>> GetAll(CancellationToken cancellationToken) => _db.Products.AsNoTracking().ToListAsync(cancellationToken);

    public Task<List<Product>> GetAll(PaginationDto pagination, CancellationToken cancellationToken) => _db.Products.AsNoTracking().ApplyPagination(pagination, cancellationToken);

    public async Task<Product?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Products.FindAsync([id], cancellationToken);
    }

    public async Task Update(Product product, CancellationToken cancellationToken)
    {
        _db.Products.Update(product);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
