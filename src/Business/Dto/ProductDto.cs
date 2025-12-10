namespace Business.Dto;

public sealed class ProductDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string ImgUri { get; set; } = null!;

    public decimal Price { get; set; }

    public string? Description { get; set; }
}
