namespace ReverseAnalytics.Domain.QueryParameters;

public class ProductQueryParameters : PaginatedQueryParameters
{
    public decimal? Price { get; set; }
    public int? CategoryId { get; set; }
}
