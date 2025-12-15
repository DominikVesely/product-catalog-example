using Business.Dto;
using Business.Exceptions;
using Business.Mappers;
using Data.Dto;
using Data.Entities;
using Data.Repositories;
using Microsoft.Extensions.Logging;

namespace Business.Services;

public sealed class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly ProductMapper _mapper;
    private readonly ILogger<ProductService> _logger;

    public ProductService(IProductRepository repository, ProductMapper mapper, ILogger<ProductService> logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<List<ProductDto>> GetAll(CancellationToken cancellationToken)
    {
        _logger.LogDebug("Retrieving all products.");
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
        _logger.LogDebug("Retrieving all products with pagination: PageNumber={PageNumber}, PageSize={PageSize}.", pagination.Page, pagination.PageSize);
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
        _logger.LogDebug("Retrieving product with ID {ProductId}.", id);
        Product? entity = await _repository.GetById(id, cancellationToken);
        if (entity is null)
        {
            _logger.LogWarning("Product with ID {ProductId} not found.", id);
            return null;
        }
        return MapToDto(entity);
    }

    public async Task<ProductDto> UpdateDescription(Guid id, string? description, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Updating description for product with ID {ProductId}.", id);
        if (id == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(id));
        }

        var entity = (await _repository.GetById(id, cancellationToken)) ?? throw new NotFoundException($"Product not found. Id={id}");
        entity.Description = description?.Trim();

        await _repository.Update(entity, cancellationToken);
        _logger.LogInformation("Product with ID {ProductId} description updated.", id);

        return MapToDto(entity);
    }

    private ProductDto MapToDto(Product p) => _mapper.MapProductToDto(p);
}
