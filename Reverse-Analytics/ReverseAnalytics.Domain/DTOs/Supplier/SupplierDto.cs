using ReverseAnalytics.Domain.DTOs.Supply;

namespace ReverseAnalytics.Domain.DTOs.Supplier;

public record SupplierDto(
    int Id,
    string FullName,
    string PhoneNumber,
    string Company,
    decimal Balance,
    ICollection<SupplyDto> Supplies);
