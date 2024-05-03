using ReverseAnalytics.Domain.Common;
using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.Entities;

public class Supply : BaseAuditableEntity, ITransaction
{
    public DateTime Date { get; set; }
    public string? Comments { get; set; }
    public decimal TotalDue { get; set; }
    public decimal TotalPaid { get; set; }
    public PaymentType PaymentType { get; set; }
    public CurrencyType Currency { get; set; }

    public int SupplierId { get; set; }
    public virtual Supplier Supplier { get; set; }

    public virtual ICollection<SupplyItem> SupplyItems { get; set; }

    public TransactionType TransactionType => TransactionType.Expense;
    public TransactionSource TransactionSource => TransactionSource.Supply;
    public decimal GetTransactionAmount() => TotalDue;
    public int GetTransactionSourceId() => Id;

    public Supply()
    {
        SupplyItems = [];
    }
}
