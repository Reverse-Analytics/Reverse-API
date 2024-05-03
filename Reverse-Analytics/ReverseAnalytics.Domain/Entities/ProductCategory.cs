using ReverseAnalytics.Domain.Common;

namespace ReverseAnalytics.Domain.Entities;

public class ProductCategory : BaseAuditableEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }

    public int? ParentId { get; set; }
    public virtual ProductCategory? Parent { get; set; }

    public virtual ICollection<Product> Products { get; set; }
    public virtual ICollection<ProductCategory> SubCategories { get; set; }

    public ProductCategory()
    {
        Products = [];
    }
}
