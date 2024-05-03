using Microsoft.EntityFrameworkCore;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Infrastructure.Repositories;

public class SaleItemRepository(ApplicationDbContext context) : RepositoryBase<SaleItem>(context), ISaleItemRepository
{
    public async Task<IEnumerable<SaleItem>> FindBySale(int saleId)
    {
        var saleItems = await _context.SaleItems
            .Where(x => x.SaleId == saleId)
            .AsNoTracking()
            .ToListAsync();

        return saleItems;
    }
}
