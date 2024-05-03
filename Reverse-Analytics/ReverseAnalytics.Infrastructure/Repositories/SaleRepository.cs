using Microsoft.EntityFrameworkCore;
using ReverseAnalytics.Domain.Common;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Domain.QueryParameters;
using ReverseAnalytics.Infrastructure.Extensions;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Infrastructure.Repositories;

public class SaleRepository(ApplicationDbContext context) : RepositoryBase<Sale>(context), ISaleRepository
{
    public async Task<PaginatedList<Sale>> FindAllAsync(SaleQueryParameters queryParameters)
    {
        ArgumentNullException.ThrowIfNull(queryParameters);

        var query = _context.Sales.AsQueryable();

        if (queryParameters.TotalDue.HasValue)
        {
            query = query.Where(x => x.TotalDue == queryParameters.TotalDue);
        }

        if (queryParameters.SaleDate.HasValue)
        {
            query = query.Where(x => x.Date == queryParameters.SaleDate);
        }

        if (queryParameters.Status.HasValue)
        {
            query = query.Where(x => x.Status == queryParameters.Status);
        }

        if (queryParameters.CustomerId.HasValue)
        {
            query = query.Where(x => x.CustomerId == queryParameters.CustomerId);
        }

        var sales = await query.AsNoTracking()
            .ToPaginatedListAsync(queryParameters.PageNumber, queryParameters.PageSize);

        return sales;
    }
}
