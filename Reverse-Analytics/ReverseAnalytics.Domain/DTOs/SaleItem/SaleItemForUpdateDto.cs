namespace ReverseAnalytics.Domain.DTOs.SaleItem;

public record SaleItemForUpdateDto(
    int Id,
    int Quantity,
    decimal UnitPrice,
    decimal Discount,
    int SaleId,
    int ProductId);
