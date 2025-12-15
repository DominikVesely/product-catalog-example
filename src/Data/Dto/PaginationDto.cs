using System.ComponentModel.DataAnnotations;

namespace Data.Dto;

public sealed class PaginationDto
{
    /// <summary>
    /// Gets or sets the current page number in a paginated result set.
    /// </summary>
    [Range(1, int.MaxValue)]
    public int Page { get; set; }

    /// <summary>
    /// Gets or sets the number of items to include on each page of results.
    /// </summary>
    [Range(1, 100)]
    public int PageSize { get; set; } = 10;
}
