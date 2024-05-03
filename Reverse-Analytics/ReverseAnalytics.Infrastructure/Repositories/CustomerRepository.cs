using Microsoft.EntityFrameworkCore;
using ReverseAnalytics.Domain.Common;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Domain.QueryParameters;
using ReverseAnalytics.Infrastructure.Extensions;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Infrastructure.Repositories;

public class CustomerRepository(ApplicationDbContext context) : RepositoryBase<Customer>(context), ICustomerRepository
{
    public async Task<PaginatedList<Customer>> FindAllAsync(CustomerQueryParameters queryParameters)
    {
        ArgumentNullException.ThrowIfNull(queryParameters);

        var query = _context.Customers.AsQueryable();

        if (queryParameters.Balance.HasValue)
        {
            query = query.Where(x => x.Balance == queryParameters.Balance);
        }

        if (!string.IsNullOrWhiteSpace(queryParameters.SearchQuery))
        {
            query = query.Where(x => x.FirstName.Contains(queryParameters.SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                x.LastName.Contains(queryParameters.SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                (x.Company != null && x.Company.Contains(queryParameters.SearchQuery, StringComparison.OrdinalIgnoreCase)) ||
                (x.Address != null && x.Address.Contains(queryParameters.SearchQuery, StringComparison.OrdinalIgnoreCase)));
        }

        var customers = await query.AsNoTracking()
            .ToPaginatedListAsync(queryParameters.PageNumber, queryParameters.PageSize);

        return customers;
    }
}
