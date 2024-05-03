using ReverseAnalytics.Domain.DTOs.Sale;
using ReverseAnalytics.Domain.DTOs.Supply;
using ReverseAnalytics.Domain.DTOs.SupplyItem;
using ReverseAnalytics.Domain.QueryParameters;

namespace ReverseAnalytics.Domain.Interfaces.Services;

public interface ISupplyService
{
    Task<IEnumerable<SupplyDto>> GetAllAsync();
    Task<IEnumerable<SupplyDto>> GetAllAsync(SupplyQueryParameters queryParameters);
    Task<IEnumerable<SupplyItemDto>> GetItemsAsync(int supplyId);
    Task<SupplyDto> GetByIdAsync(int id);
    Task<SupplyDto> CreateAsync(SupplyForCreateDto supplyToCreate);
    Task<SupplyDto> UpdateAsync(SupplyForUpdateDto supplyToUpdate);
    Task<SupplyDto> DeleteAsync(int id);
}
