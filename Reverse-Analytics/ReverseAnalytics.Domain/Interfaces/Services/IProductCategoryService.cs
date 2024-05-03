using ReverseAnalytics.Domain.Common;
using ReverseAnalytics.Domain.DTOs.ProductCategory;
using ReverseAnalytics.Domain.QueryParameters;

namespace ReverseAnalytics.Domain.Interfaces.Services;

public interface IProductCategoryService
{
    Task<IEnumerable<ProductCategoryDto>> GetAllAsync();
    Task<(IEnumerable<ProductCategoryDto> Data, PaginationMetaData PaginationMetaData)> GetAllAsync(ProductCategoryQueryParameters queryParameters);
    Task<IEnumerable<ProductCategoryDto>> GetAllByParentIdAsync(int parentId);
    Task<ProductCategoryDto?> GetByIdAsync(int id);
    Task<ProductCategoryDto> CreateAsync(ProductCategoryForCreateDto categoryToCreate);
    Task<ProductCategoryDto> UpdateAsync(ProductCategoryForUpdateDto categoryToUpdate);
    Task DeleteAsync(int id);
}
