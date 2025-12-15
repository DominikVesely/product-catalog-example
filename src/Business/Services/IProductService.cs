using Business.Dto;
using Data.Dto;

namespace Business.Services;

public interface IProductService
{
    Task<List<ProductDto>> GetAll(CancellationToken cancellationToken);
    Task<List<ProductDto>> GetAll(PaginationDto pagination, CancellationToken cancellationToken);
    Task<ProductDto?> GetById(Guid id, CancellationToken cancellationToken);
    Task<ProductDto> UpdateDescription(Guid id, string? description, CancellationToken cancellationToken);
}
