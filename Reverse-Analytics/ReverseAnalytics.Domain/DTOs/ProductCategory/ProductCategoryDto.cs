using ReverseAnalytics.Domain.DTOs.Product;

namespace ReverseAnalytics.Domain.DTOs.ProductCategory;

public record ProductCategoryDto(
    int Id,
    string Name,
    string? Description,
    int? ParentId,
    string? ParentName,
    ICollection<ProductDto> Products,
    ICollection<ProductCategoryDto> SubCategories);
