using Business.Dto;
using Business.Exceptions;
using Business.Mappers;
using Data.Dto;
using Data.Entities;
using Data.Repositories;

namespace Business.Services;

public sealed class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly ProductMapper _mapper;

    public ProductService(IProductRepository repository, ProductMapper mapper)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<List<ProductDto>> GetAll(CancellationToken cancellationToken)
    {
        List<Product> entities = await _repository.GetAll(cancellationToken);
        List<ProductDto> result = new List<ProductDto>(entities.Count);

        foreach (Product entity in entities)
        {
            result.Add(MapToDto(entity));
        }

        return result;
    }

    public async Task<List<ProductDto>> GetAll(PaginationDto pagination, CancellationToken cancellationToken)
    {
        List<Product> entities = await _repository.GetAll(pagination, cancellationToken);
        List<ProductDto> result = new List<ProductDto>(entities.Count);

        foreach (Product entity in entities)
        {
            result.Add(MapToDto(entity));
        }

        return result;
    }

    public async Task<ProductDto?> GetById(Guid id, CancellationToken cancellationToken)
    {
        Product? entity = await _repository.GetById(id, cancellationToken);
        if (entity is null)
        {
            return null;
        }

        return MapToDto(entity);
    }

    public async Task<ProductDto> UpdateDescription(Guid id, string? description, CancellationToken cancellationToken)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(id));
        }

        var entity = (await _repository.GetById(id, cancellationToken)) ?? throw new NotFoundException($"Product not found. Id={id}");
        entity.Description = description?.Trim();

        await _repository.Update(entity, cancellationToken);

        return MapToDto(entity);
    }

    private ProductDto MapToDto(Product p) => _mapper.MapProductToDto(p);
}
