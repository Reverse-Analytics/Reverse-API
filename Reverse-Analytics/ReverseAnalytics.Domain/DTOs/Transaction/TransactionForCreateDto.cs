using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.DTOs.Transaction;

public record TransactionForCreateDto(
    DateTime Date,
    int? SourceId,
    decimal Amount,
    TransactionType Type,
    TransactionSource? Source);
