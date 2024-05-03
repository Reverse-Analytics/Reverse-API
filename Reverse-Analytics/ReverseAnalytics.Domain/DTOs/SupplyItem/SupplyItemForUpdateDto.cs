namespace ReverseAnalytics.Domain.DTOs.SupplyItem;

public record SupplyItemForUpdateDto(
    int Id,
    int Quantity,
    decimal UnitPrice,
    decimal Discount,
    int SupplyId,
    int ProductId);
