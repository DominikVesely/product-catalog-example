using Data.Dto;
using Microsoft.EntityFrameworkCore;

namespace Business.Extensions;

public static class PaginationExtensions
{
    public static List<T> ApplyPagination<T>(this ICollection<T> queryable, PaginationDto pagination)
    {
        int skip = (pagination.Page - 1) * pagination.PageSize;
        return queryable.Skip(skip)
            .Take(pagination.PageSize)
            .ToList();
    }

    public static Task<List<T>> ApplyPagination<T>(this IQueryable<T> queryable, PaginationDto pagination, CancellationToken cancellationToken)
    {
        int skip = (pagination.Page - 1) * pagination.PageSize;
        return queryable.Skip(skip)
            .Take(pagination.PageSize)
            .ToListAsync(cancellationToken);
    }
}
