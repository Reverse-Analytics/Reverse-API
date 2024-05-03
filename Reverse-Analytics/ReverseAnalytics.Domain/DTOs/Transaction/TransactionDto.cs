using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.DTOs.Transaction;

public record TransactionDto(
    int Id,
    DateTime Date,
    int? SourceId,
    decimal Amount,
    TransactionType Type,
    TransactionSource? Source);
