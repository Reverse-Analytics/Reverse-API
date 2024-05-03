using ReverseAnalytics.Domain.DTOs.SupplyItem;
using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.DTOs.Sale;

public record SupplyForCreateDto(
    DateTime Date,
    string? Comments,
    decimal TotalPaid,
    PaymentType PaymentType,
    CurrencyType CurrencyType,
    int SupplierId,
    ICollection<SupplyItemForCreateDto> SupplyItems);
