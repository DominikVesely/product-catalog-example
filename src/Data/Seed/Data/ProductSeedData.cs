using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Seed.Data;

internal class ProductSeedData : ISeeder
{
    private static IReadOnlyList<Product> Data => new List<Product>
    {
        new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "USB-C Charger", ImgUri = "https://example.com/imgs/charger1.jpg", Price = 19.99m, Description = "Fast 30W USB-C charger." },
        new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000002"), Name = "Wireless Mouse", ImgUri = "https://example.com/imgs/mouse2.jpg", Price = 29.50m, Description = "Ergonomic wireless mouse." },
        new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000003"), Name = "Mechanical Keyboard", ImgUri = "https://example.com/imgs/keyboard3.jpg", Price = 89.00m, Description = "Compact mechanical keyboard." },
        new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000004"), Name = "Bluetooth Speaker", ImgUri = "https://example.com/imgs/speaker4.jpg", Price = 45.00m, Description = "Portable bluetooth speaker." },
        new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000005"), Name = "Webcam HD", ImgUri = "https://example.com/imgs/webcam5.jpg", Price = 59.99m, Description = "1080p webcam." },
        new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000006"), Name = "Noise Cancelling Headphones", ImgUri = "https://example.com/imgs/headphones6.jpg", Price = 129.99m, Description = "Over-ear noise cancelling." },
        new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000007"), Name = "External SSD 1TB", ImgUri = "https://example.com/imgs/ssd7.jpg", Price = 119.00m, Description = "Fast external SSD." },
        new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000008"), Name = "Laptop Stand", ImgUri = "https://example.com/imgs/stand8.jpg", Price = 34.99m, Description = "Aluminium laptop stand." },
        new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000009"), Name = "USB Hub", ImgUri = "https://example.com/imgs/hub9.jpg", Price = 24.50m, Description = "4-port USB hub." },
        new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000010"), Name = "Smartwatch", ImgUri = "https://example.com/imgs/watch10.jpg", Price = 199.99m, Description = "Fitness smartwatch." },
        new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000011"), Name = "Wireless Earbuds", ImgUri = "https://example.com/imgs/earbuds11.jpg", Price = 79.99m, Description = "True wireless earbuds." },
        new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000012"), Name = "Portable Monitor", ImgUri = "https://example.com/imgs/monitor12.jpg", Price = 229.00m, Description = "15.6\" portable monitor." },
        new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000013"), Name = "Gaming Mousepad", ImgUri = "https://example.com/imgs/mousepad13.jpg", Price = 19.00m, Description = "Large cloth mousepad." },
        new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000014"), Name = "HDMI Cable 2m", ImgUri = "https://example.com/imgs/hdmi14.jpg", Price = 9.99m, Description = "High-speed HDMI cable." },
        new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000015"), Name = "Router AC1200", ImgUri = "https://example.com/imgs/router15.jpg", Price = 69.99m, Description = "Dual-band router." },
        new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000016"), Name = "Wireless Keyboard", ImgUri = "https://example.com/imgs/keyboard16.jpg", Price = 49.99m, Description = "Slim wireless keyboard." },
        new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000017"), Name = "Power Bank 20k", ImgUri = "https://example.com/imgs/powerbank17.jpg", Price = 39.99m, Description = "20000mAh power bank." },
        new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000018"), Name = "Tablet Case", ImgUri = "https://example.com/imgs/case18.jpg", Price = 14.99m, Description = "Protective tablet case." },
        new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000019"), Name = "SD Card 128GB", ImgUri = "https://example.com/imgs/sdcard19.jpg", Price = 24.99m, Description = "High-speed SD card." },
        new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000020"), Name = "HD Projector", ImgUri = "https://example.com/imgs/projector20.jpg", Price = 299.00m, Description = "Portable HD projector." }
    };

    public async Task SeedAsync(AppDbContext context, IServiceProvider services)
    {
        if (await context.Products.AnyAsync()) return;

        context.Products.AddRange(Data);
        await context.SaveChangesAsync();
    }
}