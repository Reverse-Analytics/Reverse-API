using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Interfaces.Repositories;

public interface ISaleItemRepository : IRepositoryBase<SaleItem>
{
    Task<IEnumerable<SaleItem>> FindBySale(int saleId);
}
