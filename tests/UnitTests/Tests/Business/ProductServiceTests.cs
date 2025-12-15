using Business.Dto;
using Business.Exceptions;
using Business.Mappers;
using Business.Services;
using Data.Entities;
using Data.Repositories;
using FluentAssertions;
using NSubstitute;

namespace UnitTests.Tests.Business;

public class ProductServiceTests
{
    private readonly ProductService _sut;
    private readonly IProductRepository _repositorySub;

    public ProductServiceTests()
    {
        _repositorySub = Substitute.For<IProductRepository>();
        _sut = new ProductService(_repositorySub, new ProductMapper());
    }

    public static TheoryData<Guid, string?, string?, string?> UpdateSuccessTestData => new()
    {
        // new non-empty description, original null
        { new("00000000-0000-0000-0000-000000000010"), null, "New description", "New description" },

        // trimming whitespace
        { new("00000000-0000-0000-0000-000000000011"), "old", "  trimmed  ", "trimmed" },

        // replacing existing description
        { new("00000000-0000-0000-0000-000000000012"), "previous", "updated", "updated" },

        // whitespace-only becomes empty string
        { new("00000000-0000-0000-0000-000000000013"), "x", "   ", "" },

        { new("00000000-0000-0000-0000-000000000014"), "original", null, null },
    };

    [Theory]
    [MemberData(nameof(UpdateSuccessTestData))]
    public async Task UpdateDescription_Success_UpdatesEntityAndReturnsDto(Guid id, string? originalDescription, string? newDescription, string? expectedTrimmed)
    {
        // arrange
        Product product = CreateProduct(id, originalDescription);
        _repositorySub.GetById(id, CancellationToken.None).Returns(Task.FromResult<Product?>(product));
        _repositorySub.Update(Arg.Any<Product>(), Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);

        var expected = CreateDto(id, expectedTrimmed);

        // act
        var result = await _sut.UpdateDescription(id, newDescription, CancellationToken.None);

        // assert
        await _repositorySub.Received(1).Update(Arg.Is<Product>(p => p.Description == expectedTrimmed), Arg.Any<CancellationToken>());
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task UpdateDescription_ThrowsArgumentNullException_WhenIdIsEmpty()
    {
        // act
        Func<Task> act = () => _sut.UpdateDescription(Guid.Empty, "desc", CancellationToken.None);

        // assert
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task UpdateDescription_ThrowsNotFoundException_WhenProductMissing()
    {
        // arrange
        Guid id = new("00000000-0000-0000-0000-000000000001");
        _repositorySub.GetById(id, CancellationToken.None).Returns(Task.FromResult<Product?>(null));

        // act
        Func<Task> act = () => _sut.UpdateDescription(id, "desc", CancellationToken.None);

        // assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    private static ProductDto CreateDto(Guid id, string? description)
    {
        ProductDto p = new()
        {
            Id = id,
            Name = "Product",
            Price = 1m,
            Description = description
        };
        return p;
    }

    private static Product CreateProduct(Guid id, string? description = null)
    {
        Product p = new()
        {
            Id = id,
            Name = "Product",
            Price = 1m,
            Description = description
        };
        return p;
    }
}
