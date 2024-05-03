namespace ReverseAnalytics.Domain.Common;

public record PaginationMetaData(
    int TotalCount,
    int PageSize,
    int CurrentPage,
    int PagesCount,
    bool HasNext,
    bool HasPrevious);
