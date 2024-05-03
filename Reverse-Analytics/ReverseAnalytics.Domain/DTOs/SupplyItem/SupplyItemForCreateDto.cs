namespace ReverseAnalytics.Domain.DTOs.SupplyItem;

public record SupplyItemForCreateDto(
    int Quantity,
    decimal UnitPrice,
    decimal Discount,
    int SupplyId,
    int ProductId);
