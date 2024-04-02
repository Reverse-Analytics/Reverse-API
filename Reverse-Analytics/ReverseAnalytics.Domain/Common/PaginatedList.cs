namespace ReverseAnalytics.Domain.Common;

/// <summary>
/// Represents a paginated list of items.
/// </summary>
/// <typeparam name="T">The type of items in the list.</typeparam>
public class PaginatedList<T> : List<T>
{
    /// <summary>
    /// Gets the current page number.
    /// </summary>
    public int CurrentPage { get; private set; }

    /// <summary>
    /// Gets the size of each page.
    /// </summary>
    public int PageSize { get; private set; }

    /// <summary>
    /// Gets the total number of pages.
    /// </summary>
    public int PagesCount { get; private set; }

    /// <summary>
    /// Gets the total number of items across all pages.
    /// </summary>
    public int TotalCount { get; private set; }

    /// <summary>
    /// Determines whether there is a next page.
    /// </summary>
    public bool HasNext => CurrentPage < PagesCount;

    /// <summary>
    /// Determines whether there is a previous page.
    /// </summary>
    public bool HasPrevious => CurrentPage > 1;

    /// <summary>
    /// Initializes a new instance of the <see cref="PaginatedList{T}"/> class.
    /// </summary>
    /// <param name="items">The list of items.</param>
    /// <param name="currentPage">The current page number.</param>
    /// <param name="pageSize">The size of each page.</param>
    /// <param name="totalCount">The total number of items across all pages.</param>
    /// <exception cref="ArgumentNullException">Thrown when the items list is null.</exception>
    /// <exception cref="ArgumentException">Thrown when invalid arguments are provided (e.g., negative totalCount, currentPage less than 1).</exception>
    public PaginatedList(List<T> items, int currentPage, int pageSize, int totalCount)
    {
        ArgumentNullException.ThrowIfNull(items);

        if (totalCount < 0)
            throw new ArgumentException("Total count cannot be negative.", nameof(totalCount));

        if (currentPage < 1)
            throw new ArgumentException("Current page must be greater than 0.", nameof(currentPage));

        if (pageSize < 1)
            throw new ArgumentException("Page size must be greater than 0.", nameof(pageSize));

        if (totalCount < items.Count)
            throw new ArgumentException($"Total count: {totalCount} cannot be less than items count: {items.Count}.", nameof(totalCount));

        if (items.Count == 0 && totalCount > 0)
            throw new ArgumentException($"Total count cannot be greater than 0 when items empty.", nameof(totalCount));

        TotalCount = totalCount;
        CurrentPage = currentPage;
        PageSize = pageSize;
        PagesCount = (int)Math.Ceiling((double)totalCount / pageSize);
        AddRange(items);
    }

    /// <summary>
    /// Converts the pagination information to <see cref="PaginationMetaData"/>.
    /// </summary>
    /// <returns>The pagination metadata.</returns>
    public PaginationMetaData MetaData
        => new(TotalCount,
            PageSize,
            CurrentPage,
            PagesCount,
            HasNext,
            HasPrevious);
}