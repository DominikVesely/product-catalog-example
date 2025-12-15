using Business.Extensions;
using Data.Dto;
using Data.Entities;
using Data.Repositories.Base;

namespace Data.Repositories;

public class ProductRepositoryJson : BaseRepositoryJson<Product>, IProductRepository
{
    public ProductRepositoryJson() : base("products.json")
    {
    }

    public Task<List<Product>> GetAll(CancellationToken cancellationToken)
    => Task.FromResult(_data);

    public Task<List<Product>> GetAll(PaginationDto pagination, CancellationToken cancellationToken)
        => Task.FromResult(_data.ApplyPagination(pagination));

    public Task<Product?> GetById(Guid id, CancellationToken cancellationToken)
    => Task.FromResult(_data.FirstOrDefault(p => p.Id == id));

    public Task Update(Product product, CancellationToken cancellationToken)
    {
        throw new NotSupportedException();
    }
}
