using ReverseAnalytics.Domain.DTOs.Customer;
using ReverseAnalytics.Domain.DTOs.SaleItem;
using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.DTOs.Sale;

public record SaleDto(
    int Id,
    DateTime Date,
    string? Comments,
    decimal TotalPaid,
    decimal TotalDiscount,
    SaleType SaleType,
    SaleStatus Status,
    PaymentType PaymentType,
    CurrencyType CurrencyType,
    CustomerDto Customer,
    ICollection<SaleItemDto> SaleItems);
