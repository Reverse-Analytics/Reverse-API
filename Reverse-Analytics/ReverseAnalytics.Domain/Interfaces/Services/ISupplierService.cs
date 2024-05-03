using ReverseAnalytics.Domain.DTOs.Supplier;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.QueryParameters;

namespace ReverseAnalytics.Domain.Interfaces.Services;

public interface ISupplierService
{
    Task<IEnumerable<SupplierDto>> GetAllAsync();
    Task<IEnumerable<SupplierDto>> GetAllAsync(SupplierQueryParameters queryParameters);
    Task<IEnumerable<Supply>> GetSuppliesAsync(int supplierId);
    Task<SupplierDto> GetByIdAsync(int id);
    Task<SupplierDto> CreateAsync(SupplierForCreateDto supplierToCreate);
    Task<SupplierDto> UpdateAsync(SupplierForUpdateDto supplierToUpdate);
    Task<SupplierDto> DeleteAsync(int id);
}
