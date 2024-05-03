using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.DTOs.Transaction;

public record TransactionForUpdateDto(
    int Id,
    DateTime Date,
    int? SourceId,
    decimal Amount,
    TransactionType Type,
    TransactionSource? Source);
