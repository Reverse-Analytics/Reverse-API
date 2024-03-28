namespace ReverseAnalytics.Domain.QueryParameters;

public class PaginatedQueryParameters : QueryParametersBase
{
    protected const int MAX_PAGE_SIZE = 50;
    protected const int DEFAULT_PAGE_SIZE = 15;

    public int PageNumber { get; init; } = 1;

    private int _pageSize = DEFAULT_PAGE_SIZE;
    public int PageSize
    {
        get => _pageSize;
        init
        {
            if (value > MAX_PAGE_SIZE)
            {
                _pageSize = MAX_PAGE_SIZE;

                return;
            }

            _pageSize = value;
        }
    }
}
