using ReverseAnalytics.Domain.DTOs.Product;
using ReverseAnalytics.Domain.DTOs.Sale;

namespace ReverseAnalytics.Domain.DTOs.SaleItem;

public record SaleItemDto(
    int Id,
    int Quantity,
    decimal UnitPrice,
    decimal Discount,
    SaleDto Sale,
    ProductDto Product);