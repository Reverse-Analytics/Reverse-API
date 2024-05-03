using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.DTOs.Sale;

public record SupplyForUpdateDto(
    int Id,
    DateTime Date,
    string? Comments,
    decimal TotalPaid,
    PaymentType PaymentType,
    CurrencyType CurrencyType,
    int SupplierId);
