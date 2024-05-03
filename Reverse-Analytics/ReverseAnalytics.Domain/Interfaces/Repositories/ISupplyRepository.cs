using ReverseAnalytics.Domain.Common;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.QueryParameters;

namespace ReverseAnalytics.Domain.Interfaces.Repositories;

public interface ISupplyRepository : IRepositoryBase<Supply>
{
    Task<PaginatedList<Supply>> FindAllAsync(SupplyQueryParameters queryParameters);
}
