using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.Common;

public interface ITransaction
{
    TransactionType TransactionType { get; }
    TransactionSource TransactionSource { get; }
    PaymentType PaymentType { get; }
    CurrencyType Currency { get; }
    int GetTransactionSourceId();
    decimal GetTransactionAmount();
}
