using ReverseAnalytics.Domain.DTOs.Customer;
using ReverseAnalytics.Domain.DTOs.Product;
using ReverseAnalytics.Domain.DTOs.Sale;
using ReverseAnalytics.Domain.QueryParameters;

namespace ReverseAnalytics.Domain.Interfaces.Services;

public interface ICustomerService
{
    Task<IEnumerable<CustomerDto>> GetAllAsync();
    Task<IEnumerable<CustomerDto>> GetAllAsync(ProductQueryParameters queryParameters);
    Task<IEnumerable<SaleDto>> GetSalesAsync(int customerId);
    Task<CustomerDto> GetByIdAsync(int id);
    Task<CustomerDto> CreateAsync(ProductForCreateDto productToCreate);
    Task<CustomerDto> UpdateAsync(ProductForUpdateDto productToUpdate);
    Task<CustomerDto> DeleteAsync(int id);
}
