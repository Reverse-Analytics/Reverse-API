namespace ReverseAnalytics.Domain.QueryParameters;

public class SupplyQueryParameters : PaginatedQueryParameters
{
    public DateTime? SupplyDate { get; set; }
    public decimal? TotalDue { get; set; }
    public int? SupplierId { get; set; }
}
