namespace ReverseAnalytics.Domain.QueryParameters;

public class CustomerQueryParameters : PaginatedQueryParameters
{
    public decimal? Balance { get; set; }
}
