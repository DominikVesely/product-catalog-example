using Data.Entities;

namespace Data.Repositories;

internal interface IProductRepository
{
    Task<List<Product>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task UpdateAsync(Product product, CancellationToken cancellationToken = default);

}
