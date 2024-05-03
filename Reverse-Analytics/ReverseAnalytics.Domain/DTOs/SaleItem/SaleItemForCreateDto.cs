namespace ReverseAnalytics.Domain.DTOs.SaleItem;

public record SaleItemForCreateDto(
    int Quantity,
    decimal UnitPrice,
    decimal Discount,
    int SaleId,
    int ProductId);
