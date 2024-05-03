using ReverseAnalytics.Domain.Common;
using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Domain.Entities;

public class Product : BaseAuditableEntity
{
    public string Name { get; set; }
    public string Code { get; set; }
    public string? Description { get; set; }
    public decimal SalePrice { get; set; }
    public decimal SupplyPrice { get; set; }
    public double? Volume { get; set; }
    public double? Weight { get; set; }
    public UnitOfMeasurement UnitOfMeasurement { get; set; }

    public int CategoryId { get; set; }
    public virtual ProductCategory Category { get; set; }

    public virtual ICollection<SaleItem> SaleItems { get; set; }
    public virtual ICollection<SupplyItem> SupplyItems { get; set; }

    public Product()
    {
        Name = string.Empty;
        Code = string.Empty;

        SaleItems = [];
        SupplyItems = [];
    }
}
