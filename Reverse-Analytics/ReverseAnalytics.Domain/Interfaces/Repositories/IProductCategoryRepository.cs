using ReverseAnalytics.Domain.Common;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.QueryParameters;

namespace ReverseAnalytics.Domain.Interfaces.Repositories;

public interface IProductCategoryRepository : IRepositoryBase<ProductCategory>
{
    Task<PaginatedList<ProductCategory>> FindAllAsync(ProductCategoryQueryParameters queryParameters);
    Task<IEnumerable<ProductCategory>> FindByParentIdAsync(int parentId);
}
