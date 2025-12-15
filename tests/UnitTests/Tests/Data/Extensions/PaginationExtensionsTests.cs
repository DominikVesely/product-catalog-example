using Business.Extensions;
using Data.Dto;
using FluentAssertions;
using MockQueryable.NSubstitute;

namespace UnitTests.Tests.Data.Extensions;

public class PaginationExtensionsTests
{
    [Theory]
    [MemberData(nameof(TestData))]
    public void ApplyPagination_OnCollection_ReturnsExpectedPage(List<string> actual, PaginationDto pagination, List<string> expected)
    {
        List<string> result = actual.ApplyPagination(pagination);

        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(TestData))]
    public async Task ApplyPagination_OnQueryable_ReturnsExpectedPageAsync(List<string> actual, PaginationDto pagination, List<string> expected)
    {
        List<string> result = await actual.BuildMockDbSet().ApplyPagination(pagination, CancellationToken.None);

        result.Should().Equal(expected);
    }

    public static TheoryData<List<string>, PaginationDto, List<string>> TestData => new()
    {
        // full page fits exactly
        { ["a", "b", "c"], new PaginationDto { Page = 1, PageSize = 3 }, ["a", "b", "c"] },

        // middle page
        { ["a", "b", "c", "d", "e"], new PaginationDto { Page = 2, PageSize = 2 }, ["c", "d"] },

        // page size larger than source
        { ["x", "y", "z"], new PaginationDto { Page = 1, PageSize = 5 }, ["x", "y", "z"] },

        // empty source
        { [], new PaginationDto { Page = 1, PageSize = 10 }, [] },

        // page number too large (no items)
        { ["a", "b"], new PaginationDto { Page = 10, PageSize = 2 }, [] },

        // smallest page size
        { ["a", "b", "c"], new PaginationDto { Page = 2, PageSize = 1 }, ["b"] },

        // zero page size (edge case - runtime Take(0) -> empty)
        { ["a", "b", "c"], new PaginationDto { Page = 1, PageSize = 0 }, [] },
    };
}
