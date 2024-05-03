using ReverseAnalytics.Domain.DTOs.Supplier;
using ReverseAnalytics.Domain.DTOs.SupplyItem;
using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.DTOs.Supply;

public record SupplyDto(
    int Id,
    DateTime Date,
    string? Comments,
    decimal TotalPaid,
    PaymentType PaymentType,
    CurrencyType CurrencyType,
    SupplierDto Customer,
    ICollection<SupplyItemDto> SupplyItems);
