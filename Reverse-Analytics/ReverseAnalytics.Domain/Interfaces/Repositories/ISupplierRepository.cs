using ReverseAnalytics.Domain.Common;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.QueryParameters;

namespace ReverseAnalytics.Domain.Interfaces.Repositories;

public interface ISupplierRepository : IRepositoryBase<Supplier>
{
    Task<PaginatedList<Supplier>> FindAllAsync(SupplierQueryParameters queryParameters);
}
