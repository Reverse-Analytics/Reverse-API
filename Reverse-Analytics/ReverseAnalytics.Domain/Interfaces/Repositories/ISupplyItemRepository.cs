using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Interfaces.Repositories;

public interface ISupplyItemRepository : IRepositoryBase<SupplyItem>
{
    Task<IEnumerable<SupplyItem>> FindBySupplyAsync(int supplyId);
}
