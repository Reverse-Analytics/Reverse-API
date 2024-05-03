using ReverseAnalytics.Domain.Common;

namespace ReverseAnalytics.Domain.Entities;

public class Supplier : BaseAuditableEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Company { get; set; }
    public decimal Balance { get; set; }

    public virtual ICollection<Supply> Supplies { get; set; }

    public Supplier()
    {
        Supplies = [];
    }
}
