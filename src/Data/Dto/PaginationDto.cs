using System.ComponentModel.DataAnnotations;

namespace Data.Dto;

public sealed class PaginationDto
{
    [Range(1, int.MaxValue)]
    public int Page { get; set; }

    [Range(1, 100)]
    public int PageSize { get; set; } = 10;
}
