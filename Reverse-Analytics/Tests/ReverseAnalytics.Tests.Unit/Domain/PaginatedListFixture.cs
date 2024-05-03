namespace ReverseAnalytics.Tests.Unit.Domain;

public class PaginatedListFixture
{
    [Fact]
    public void Constructor_ShouldThrowArgumentNullExceptionWhenItemsNull()
    {
        // Arrange
        List<int>? items = null;
        int currentPage = 1;
        int pageSize = 10;
        int totalCount = 20;

        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() => new PaginatedList<int>(items!, currentPage, pageSize, totalCount));
        exception.ParamName.Should().Be(nameof(items));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-5)]
    public void Constructor_ShouldThrowArgumentExceptionWhenTotalCountIsNegative(int totalCount)
    {
        // Arrange
        List<int> items = [1, 2, 3];
        int currentPage = 1;
        int pageSize = 10;

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => new PaginatedList<int>(items!, currentPage, pageSize, totalCount));
        exception.ParamName.Should().Be(nameof(totalCount));

        exception.Message.Should().Be("Total count cannot be negative. (Parameter 'totalCount')");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_ShouldThrowArgumentException_WhenCurrentPageIsLessThan1(int currentPage)
    {
        // Arrange
        var items = new List<int>();
        int pageSize = 10;
        int totalCount = 20;

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => new PaginatedList<int>(items, currentPage, pageSize, totalCount));
        exception.ParamName.Should().Be(nameof(currentPage));
        exception.Message.Should().Be("Current page must be greater than 0. (Parameter 'currentPage')");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_ShouldThrowArgumentException_WhenPageSizeIsLessThan1(int pageSize)
    {
        // Arrange
        var items = new List<int>();
        int currentPage = 1;
        int totalCount = 20;

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => new PaginatedList<int>(items, currentPage, pageSize, totalCount));
        exception.ParamName.Should().Be(nameof(pageSize));
        exception.Message.Should().Be("Page size must be greater than 0. (Parameter 'pageSize')");
    }

    [Theory]
    [InlineData(3, 2)]
    [InlineData(5, 1)]
    public void Constructor_ShouldThrowArgumentException_WhenTotalCountIsLessThanItemsCount(int itemsCount, int totalCount)
    {
        // Arrange
        var fixture = new Fixture();
        var items = fixture.CreateMany<int>(itemsCount).ToList();
        int currentPage = 1;
        int pageSize = 10;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new PaginatedList<int>(items, currentPage, pageSize, totalCount));
    }

    [Fact]
    public void Constructor_ShouldCalculatePagesCountCorrectly_WhenTotalCountIs0()
    {
        // Arrange
        var items = new List<int>();
        int currentPage = 1;
        int pageSize = 10;
        int totalCount = 0; // 0 total items

        // Act
        var paginatedList = new PaginatedList<int>(items, currentPage, pageSize, totalCount);

        // Assert
        paginatedList.PagesCount.Should().Be(0);
    }

    [Fact]
    public void Constructor_ShouldInitializePropertiesCorrectly()
    {
        // Arrange
        var items = new List<int> { 1, 2, 3 };
        int currentPage = 1;
        int pageSize = 10;
        int totalCount = 20;

        // Act
        var paginatedList = new PaginatedList<int>(items, currentPage, pageSize, totalCount);

        // Assert
        paginatedList.CurrentPage.Should().Be(currentPage);
        paginatedList.PageSize.Should().Be(pageSize);
        paginatedList.TotalCount.Should().Be(totalCount);
        paginatedList.PagesCount.Should().Be(2); // 20 total items, 10 items per page
        paginatedList.Should().BeEquivalentTo(items);
    }

    [Theory]
    [InlineData(4, 1, 10, 20, true)]
    [InlineData(5, 2, 10, 20, false)]
    public void HasNext_ShouldReturnExpectedResult(int itemsCount, int currentPage, int pageSize, int totalCount, bool expectedResult)
    {
        // Arrange
        var fixture = new Fixture();
        var items = fixture.CreateMany<int>(itemsCount).ToList();
        var paginatedList = new PaginatedList<int>(items, currentPage, pageSize, totalCount);

        // Act & Assert
        paginatedList.HasNext.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData(4, 2, 10, 20, true)]
    [InlineData(5, 1, 10, 20, false)]
    public void HasPrevious_ShouldReturnExpectedResult(int itemsCount, int currentPage, int pageSize, int totalCount, bool expectedResult)
    {
        // Arrange
        var fixture = new Fixture();
        var items = fixture.CreateMany<int>(itemsCount).ToList();
        var paginatedList = new PaginatedList<int>(items, currentPage, pageSize, totalCount);

        // Act & Assert
        paginatedList.HasPrevious.Should().Be(expectedResult);
    }

    [Fact]
    public void Constructor_ShouldAddElementsToList()
    {
        // Arrange
        var fixture = new Fixture();
        var items = fixture.CreateMany<int>(5).ToList();

        // Act
        var paginatedList = new PaginatedList<int>(items, 1, 10, 20);

        // Assert
        paginatedList.Should().BeEquivalentTo(items);
    }

    [Fact]
    public void ToMetaData_ShouldReturnCorrectPaginationMetaData()
    {
        // Arrange
        var items = new List<int> { 1, 2, 3 };
        var paginatedList = new PaginatedList<int>(items, 1, 10, 20);

        // Act
        var metaData = paginatedList.MetaData;

        // Assert
        metaData.TotalCount.Should().Be(20);
        metaData.PageSize.Should().Be(10);
        metaData.CurrentPage.Should().Be(1);
        metaData.PagesCount.Should().Be(2);
        metaData.HasNext.Should().BeTrue();
        metaData.HasPrevious.Should().BeFalse();
    }
}
