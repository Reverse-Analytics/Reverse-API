using Microsoft.EntityFrameworkCore;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Infrastructure.Repositories;

public class SupplyItemRepository(ApplicationDbContext context) : RepositoryBase<SupplyItem>(context), ISupplyItemRepository
{
    public async Task<IEnumerable<SupplyItem>> FindBySupplyAsync(int supplyId)
    {
        var supplies = await _context.SupplyItems
            .Where(x => x.SupplyId == supplyId)
            .AsNoTracking()
            .ToListAsync();

        return supplies;
    }
}
