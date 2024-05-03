using Microsoft.EntityFrameworkCore;
using ReverseAnalytics.Domain.Common;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Domain.QueryParameters;
using ReverseAnalytics.Infrastructure.Extensions;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Infrastructure.Repositories;

public class SupplierRepository(ApplicationDbContext context) : RepositoryBase<Supplier>(context), ISupplierRepository
{
    public async Task<PaginatedList<Supplier>> FindAllAsync(SupplierQueryParameters queryParameters)
    {
        ArgumentNullException.ThrowIfNull(queryParameters);

        var query = _context.Suppliers.AsQueryable();

        if (queryParameters.Balance.HasValue)
        {
            query = query.Where(x => x.Balance == queryParameters.Balance);
        }

        if (!string.IsNullOrWhiteSpace(queryParameters.SearchQuery))
        {
            query = query.Where(x => x.FirstName.Contains(queryParameters.SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                x.LastName.Contains(queryParameters.SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                x.PhoneNumber.Contains(queryParameters.SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                x.Company.Contains(queryParameters.SearchQuery, StringComparison.OrdinalIgnoreCase));
        }

        var suppliers = await query.AsNoTracking()
            .ToPaginatedListAsync(queryParameters.PageNumber, queryParameters.PageSize);

        return suppliers;
    }
}
