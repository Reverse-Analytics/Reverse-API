using ReverseAnalytics.Domain.DTOs.Sale;
using ReverseAnalytics.Domain.DTOs.SaleItem;
using ReverseAnalytics.Domain.QueryParameters;

namespace ReverseAnalytics.Domain.Interfaces.Services;

public interface ISaleService
{
    Task<IEnumerable<SaleDto>> GetAllAsync();
    Task<IEnumerable<SaleDto>> GetAllAsync(SaleQueryParameters queryParameters);
    Task<IEnumerable<SaleItemDto>> GetItemsAsync(int saleId);
    Task<SaleDto> GetByIdAsync(int id);
    Task<SaleDto> CreateAsync(SaleForCreateDto saleToCreate);
    Task<SaleDto> UpdateAsync(SaleForUpdateDto saleToUpdate);
    Task<SaleDto> DeleteAsync(int id);
}
