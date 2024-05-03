using ReverseAnalytics.Domain.DTOs.SaleItem;
using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.DTOs.Sale;

public record SaleForCreateDto(
    DateTime Date,
    string? Comments,
    decimal TotalPaid,
    decimal TotalDiscount,
    SaleType SaleType,
    SaleStatus Status,
    PaymentType PaymentType,
    CurrencyType CurrencyType,
    int CustomerId,
    ICollection<SaleItemForCreateDto> SaleItems);
