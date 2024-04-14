namespace ReverseAnalytics.Tests.Unit.Controllers;

public abstract class ControllerFixtureBase : FixtureBase
{
    protected readonly ControllerContext _controllerContext;
    protected readonly HttpResponse _response;

    protected ControllerFixtureBase()
    {
        _controllerContext = GetControllerContext();
        _response = _controllerContext.HttpContext.Response;
    }

    private static ControllerContext GetControllerContext()
    {
        var httpContext = new DefaultHttpContext();
        var controllerContext = new ControllerContext { HttpContext = httpContext };

        return controllerContext;
    }

    /// <summary>
    /// Creates a PaginatedList with randomized data for 
    /// testing purposes while ensuring valid pagination parameters.
    /// This method prevents potential errors caused by AutoFixture
    /// when creating PaginatedList with invalid constructor params,
    /// such as Items length being more than the TotalCount value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    protected PaginatedList<T> CreatePaginatedList<T>()
    {
        // Generate a list of items using AutoFixture
        var items = _fixture.CreateMany<T>().ToList();

        // Ensure totalCount is greater than or equal to the count of items
        var totalCount = _fixture.Create<int>() + items.Count;

        // Generate currentPage and pageSize
        var currentPage = _fixture.Create<int>();
        var pageSize = _fixture.Create<int>();

        // Create and return the PaginatedList instance
        return new PaginatedList<T>(items, currentPage, pageSize, totalCount);
    }
}
