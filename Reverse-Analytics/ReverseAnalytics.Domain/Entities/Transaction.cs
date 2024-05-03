using ReverseAnalytics.Domain.Common;
using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.Entities;

public class Transaction : BaseAuditableEntity
{
    public DateTime Date { get; set; }
    public int? SourceId { get; set; }
    public decimal Amount { get; set; }
    public TransactionType Type { get; set; }
    public TransactionSource? Source { get; set; }
}
