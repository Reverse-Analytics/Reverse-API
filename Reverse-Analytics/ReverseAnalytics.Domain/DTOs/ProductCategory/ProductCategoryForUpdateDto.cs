namespace ReverseAnalytics.Domain.DTOs.ProductCategory;

public record ProductCategoryForUpdateDto(
    int Id,
    string Name,
    string? Description,
    int? ParentId);
