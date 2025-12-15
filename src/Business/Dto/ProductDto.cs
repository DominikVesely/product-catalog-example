using Data.Entities;

namespace Business.Dto;

public sealed class ProductDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string ImgUri { get; set; } = null!;

    public decimal Price { get; set; }

    public string? Description { get; set; }

    private static ProductDto MapToDto(Product p) =>
        new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            ImgUri = p.ImgUri ?? string.Empty,
            Price = p.Price,
            Description = p.Description
        };
}
