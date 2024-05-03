namespace ReverseAnalytics.Domain.DTOs.ProductCategory;

public record ProductCategoryForCreateDto(
    string Name,
    string? Description,
    int? ParentId,
    ICollection<ProductCategoryForCreateDto>? SubCategories);
