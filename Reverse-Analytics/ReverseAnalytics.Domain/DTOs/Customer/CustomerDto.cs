using ReverseAnalytics.Domain.DTOs.Supply;

namespace ReverseAnalytics.Domain.DTOs.Customer;

public record CustomerDto(
    int Id,
    string FullName,
    string PhoneNumber,
    string? Address,
    string? Company,
    decimal Balance,
    double Discount,
    ICollection<SupplyDto> Sales);
