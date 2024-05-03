using ReverseAnalytics.Domain.DTOs.Product;
using ReverseAnalytics.Domain.QueryParameters;

namespace ReverseAnalytics.Domain.Interfaces.Services;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllAsync();
    Task<IEnumerable<ProductDto>> GetAllAsync(ProductQueryParameters queryParameters);
    Task<IEnumerable<ProductDto>> GetByCategoryAsync(int categoryId);
    Task<ProductDto> GetByIdAsync(int id);
    Task<ProductDto> CreateAsync(ProductForCreateDto productToCreate);
    Task<ProductDto> UpdateAsync(ProductForUpdateDto productToUpdate);
    Task DeleteAsync(int id);
}
