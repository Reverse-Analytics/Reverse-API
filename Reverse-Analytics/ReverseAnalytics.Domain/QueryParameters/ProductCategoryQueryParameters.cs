namespace ReverseAnalytics.Domain.QueryParameters;

public class ProductCategoryQueryParameters : PaginatedQueryParameters
{
    public int? ParentId { get; set; }
}
