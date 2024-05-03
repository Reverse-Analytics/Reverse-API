using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.DTOs.Sale;

public record SaleForUpdateDto(
    int Id,
    DateTime Date,
    string? Comments,
    decimal TotalDue,
    decimal TotalPaid,
    decimal TotalDiscount,
    SaleType SaleType,
    SaleStatus Status,
    PaymentType PaymentType,
    CurrencyType CurrencyType,
    int CustomerId);
