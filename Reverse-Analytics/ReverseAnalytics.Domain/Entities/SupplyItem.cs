using ReverseAnalytics.Domain.Common;

namespace ReverseAnalytics.Domain.Entities;

public class SupplyItem : BaseAuditableEntity
{
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public int SupplyId { get; set; }
    public virtual Supply Supply { get; set; }

    public int ProductId { get; set; }
    public virtual Product Product { get; set; }
}
