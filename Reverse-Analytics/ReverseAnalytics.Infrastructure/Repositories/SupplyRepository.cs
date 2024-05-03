using Microsoft.EntityFrameworkCore;
using ReverseAnalytics.Domain.Common;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Domain.QueryParameters;
using ReverseAnalytics.Infrastructure.Extensions;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Infrastructure.Repositories;

public class SupplyRepository(ApplicationDbContext context) : RepositoryBase<Supply>(context), ISupplyRepository
{
    public async Task<PaginatedList<Supply>> FindAllAsync(SupplyQueryParameters queryParameters)
    {
        ArgumentNullException.ThrowIfNull(queryParameters);

        var query = _context.Supplies.AsQueryable();

        if (queryParameters.TotalDue.HasValue)
        {
            query = query.Where(x => x.TotalDue == queryParameters.TotalDue);
        }

        if (queryParameters.SupplyDate.HasValue)
        {
            query = query.Where(x => x.Date == queryParameters.SupplyDate);
        }

        if (queryParameters.SupplierId.HasValue)
        {
            query = query.Where(x => x.SupplierId == queryParameters.SupplierId);
        }

        var supplies = await query.AsNoTracking()
            .ToPaginatedListAsync(queryParameters.PageNumber, queryParameters.PageSize);

        return supplies;
    }
}
