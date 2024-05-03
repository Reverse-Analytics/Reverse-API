using ReverseAnalytics.Domain.Common;

namespace ReverseAnalytics.Domain.Entities;

public class SaleItem : BaseAuditableEntity
{
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }

    public int SaleId { get; set; }
    public virtual Sale Sale { get; set; }

    public int ProductId { get; set; }
    public virtual Product Product { get; set; }
}