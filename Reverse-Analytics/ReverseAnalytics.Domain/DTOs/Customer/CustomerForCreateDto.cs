namespace ReverseAnalytics.Domain.DTOs.Customer;

public record CustomerForCreateDto(
    string FirstName,
    string LastName,
    string PhoneNumber,
    string? Address,
    string? Company,
    double Discount);
