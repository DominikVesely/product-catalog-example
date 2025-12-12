namespace Data.Entities;

public sealed class Product
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;

    public string? ImgUri { get; set; }

    public decimal Price { get; set; }

    public string? Description { get; set; }
}