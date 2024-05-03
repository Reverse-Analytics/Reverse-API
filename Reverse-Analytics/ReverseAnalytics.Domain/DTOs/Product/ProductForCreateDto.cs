using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.DTOs.Product;

public record ProductForCreateDto(
    string Name,
    string Code,
    string? Description,
    decimal SalePrice,
    decimal SupplyPrice,
    double? Volume,
    double? Weight,
    UnitOfMeasurement UnitOfMeasurement,
    int CategoryId);
