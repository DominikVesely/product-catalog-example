using Data.Dto;
using Data.Entities;

namespace Data.Repositories;

public interface IProductRepository
{
    Task<List<Product>> GetAll(CancellationToken cancellationToken);
    Task<List<Product>> GetAll(PaginationDto pagination, CancellationToken cancellationToken);

    Task<Product?> GetById(Guid id, CancellationToken cancellationToken);

    Task Update(Product product, CancellationToken cancellationToken);

}
