using ReverseAnalytics.Domain.Common;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.QueryParameters;

namespace ReverseAnalytics.Domain.Interfaces.Repositories;

public interface IProductRepository : IRepositoryBase<Product>
{
    Task<PaginatedList<Product>> FindAllAsync(ProductQueryParameters queryParameters);
    Task<IEnumerable<Product>> FindByCategoryIdAsync(int categoryId);
}
