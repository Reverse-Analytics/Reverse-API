namespace ReverseAnalytics.Domain.DTOs.Customer;

public record CustomerForUpdateDto(
    int Id,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string? Address,
    string? Company,
    decimal Balance,
    double Discount);
