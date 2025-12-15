using Data;
using Data.Dto;
using Data.Entities;
using Data.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using UnitTests.Utils;

namespace UnitTests.Tests.Data.Repositories;

public class ProductRepositoryEFTests
{
    private readonly ProductRepositoryEF _sut;
    private readonly AppDbContext _contextSub;

    public ProductRepositoryEFTests()
    {
        _contextSub = MockUtils.MockDbContext();

        _contextSub.MockDbSet(c => c.Products, LoadData());

        _sut = new ProductRepositoryEF(_contextSub);
    }

    private List<Product> LoadData() =>
    [
        CreateProduct(1),
        CreateProduct(2),
        CreateProduct(3),
        CreateProduct(4),
    ];
    private static string ProductName(int index) => $"Product{index:00}";
    private static Guid ProductId(int index) => new($"00000000-0000-0000-0000-{index:000000000000}");

    [Fact]
    public async Task GetAll_ReturnsAllProductsAsync()
    {
        var expected = LoadData();
        List<Product> result = await _sut.GetAll(CancellationToken.None);

        result.Should().HaveCount(expected.Count);
        result.Select(r => r.Id).Should().BeEquivalentTo(expected.Select(p => p.Id));
    }

    [Fact]
    public async Task GetAll_WithPagination_ReturnsPagedResultsAsync()
    {
        var expected = LoadData();
        var pagination = new PaginationDto { Page = 2, PageSize = 2 };

        List<Product> page = await _sut.GetAll(pagination, CancellationToken.None);

        page.Should().HaveCount(2);
        page.Select(x => x.Name).Should().BeEquivalentTo([ProductName(3), ProductName(4)]);
    }

    [Fact]
    public async Task GetById_ReturnsProduct_WhenExistsAsync()
    {
        // act
        await _sut.GetById(ProductId(0), CancellationToken.None);

        // assert
        await _contextSub.Products.Received(1).FindAsync(Arg.Is<object[]>(ids => (Guid)ids[0] == ProductId(0)), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Update_PersistsChangesAsync()
    {
        // act
        var product = new Product();
        await _sut.Update(product, CancellationToken.None);

        // assert
        _contextSub.Products.Received(1).Update(product);
        await _contextSub.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    private static Product CreateProduct(int i, Action<Product>? options = null)
    {
        var p = new Product
        {
            Id = ProductId(i),
            Name = ProductName(i),
            Price = i * 10,
        };
        options?.Invoke(p);
        return p;
    }
}
