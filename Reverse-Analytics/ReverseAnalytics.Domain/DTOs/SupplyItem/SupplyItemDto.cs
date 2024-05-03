using ReverseAnalytics.Domain.DTOs.Product;
using ReverseAnalytics.Domain.DTOs.Supply;

namespace ReverseAnalytics.Domain.DTOs.SupplyItem;

public record SupplyItemDto(
    int Id,
    int Quantity,
    decimal UnitPrice,
    decimal Discount,
    SupplyDto Sale,
    ProductDto Product);