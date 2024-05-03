using ReverseAnalytics.Domain.Common;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.QueryParameters;

namespace ReverseAnalytics.Domain.Interfaces.Repositories;

public interface ISaleRepository : IRepositoryBase<Sale>
{
    Task<PaginatedList<Sale>> FindAllAsync(SaleQueryParameters queryParameters);
}
