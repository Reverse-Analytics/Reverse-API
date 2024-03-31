using Microsoft.EntityFrameworkCore;
using ReverseAnalytics.Domain.Common;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Domain.QueryParameters;
using ReverseAnalytics.Infrastructure.Extensions;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Infrastructure.Repositories;

public class ProductCategoryRepository(ApplicationDbContext context) : RepositoryBase<ProductCategory>(context), IProductCategoryRepository
{
    public async Task<PaginatedList<ProductCategory>> FindAllAsync(ProductCategoryQueryParameters queryParameters)
    {
        ArgumentNullException.ThrowIfNull(queryParameters);

        var query = _context.ProductCategories.AsQueryable();

        if (!string.IsNullOrWhiteSpace(queryParameters.SearchQuery))
        {
            query = query.Where(x => x.Name.Contains(queryParameters.SearchQuery, StringComparison.OrdinalIgnoreCase));
        }

        if (queryParameters.ParentId is not null)
        {
            query = query.Where(x => x.ParentId == queryParameters.ParentId);
        }

        var entities = await query
            .Include(x => x.Products)
            .Include(x => x.Parent)
            .Include(x => x.SubCategories)
            .AsNoTrackingWithIdentityResolution()
            .ToPaginatedListAsync(queryParameters.PageNumber, queryParameters.PageSize);

        return entities;
    }

    public async Task<IEnumerable<ProductCategory>> FindByParentIdAsync(int parentId)
    {
        var categories = await _context.ProductCategories
            .Where(x => x.ParentId == parentId)
            .AsNoTracking()
            .ToListAsync();

        return categories;
    }
}
