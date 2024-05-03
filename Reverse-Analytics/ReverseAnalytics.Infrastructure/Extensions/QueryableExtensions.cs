using Microsoft.EntityFrameworkCore;
using ReverseAnalytics.Domain.Common;

namespace ReverseAnalytics.Infrastructure.Extensions;

internal static class QueryableExtensions
{
    public static async Task<PaginatedList<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, int pageNumber, int pageSize)
    {
        ArgumentNullException.ThrowIfNull(source);

        var count = await source.CountAsync();
        var items = await source.Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedList<T>(items, pageNumber, pageSize, count);
    }
}
