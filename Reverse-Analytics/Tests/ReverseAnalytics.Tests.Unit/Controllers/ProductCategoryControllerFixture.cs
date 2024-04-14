namespace ReverseAnalytics.Tests.Unit.Controllers;

public class ProductCategoryControllerTests : ControllerFixtureBase
{
    private readonly Mock<IProductCategoryService> _mockCategoryService;
    private readonly Mock<IProductService> _mockProductService;
    private readonly Mock<IValidator<ProductCategoryForUpdateDto>> _mockValidator;
    private readonly ProductCategoryController _controller;

    public ProductCategoryControllerTests()
        : base()
    {
        _mockCategoryService = new Mock<IProductCategoryService>();
        _mockProductService = new Mock<IProductService>();
        _mockValidator = new Mock<IValidator<ProductCategoryForUpdateDto>>();

        _controller = new ProductCategoryController(_mockProductService.Object, _mockCategoryService.Object, _mockValidator.Object)
        {
            ControllerContext = _controllerContext
        };
    }

    [Fact]
    public async Task GetCategoriesAsync_ShouldReturnAllCategories_WhenCalled()
    {
        // Arrange
        var queryParameters = _fixture.Create<ProductCategoryQueryParameters>();
        var paginatedCategories = CreatePaginatedList<ProductCategoryDto>();
        var metadata = paginatedCategories.MetaData;

        _mockCategoryService.Setup(x => x.GetAllAsync(queryParameters)).ReturnsAsync((paginatedCategories, metadata));

        // Act
        var result = await _controller.GetCategoriesAsync(queryParameters);
        var okResult = result.Result as OkObjectResult;

        // Assert
        okResult.Should().NotBeNull();
        okResult!.StatusCode.Should().Be(((int)HttpStatusCode.OK));
        okResult!.Value.Should().BeAssignableTo<IEnumerable<ProductCategoryDto>>();
        okResult!.Value.Should().BeEquivalentTo(paginatedCategories);
    }

    [Fact]
    public async Task GetCategoriesAsync_ShouldAddPaginationHeaders_WhenCalled()
    {
        // Arrange
        var queryParameters = _fixture.Create<ProductCategoryQueryParameters>();
        var categories = new List<ProductCategoryDto>();
        var metadata = _fixture.Create<PaginationMetaData>();

        _mockCategoryService.Setup(x => x.GetAllAsync(queryParameters)).ReturnsAsync((categories, metadata));

        // Act
        _ = await _controller.GetCategoriesAsync(queryParameters);
        var actualHeaderValue = _response.Headers["X-Pagination"].FirstOrDefault();
        var expectedHeaderValue = JsonConvert.SerializeObject(metadata, Formatting.Indented);

        // Assert
        actualHeaderValue.Should().NotBeNull();
        actualHeaderValue.Should().Be(expectedHeaderValue);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnCategoryDto_WhenCalled()
    {
        // Arrange
        var categoryId = _fixture.Create<int>();
        var categoryDto = _fixture.Build<ProductCategoryDto>()
                                  .With(x => x.Id, categoryId)
                                  .Create();

        _mockCategoryService.Setup(x => x.GetByIdAsync(categoryId)).ReturnsAsync(categoryDto);

        // Act
        var result = await _controller.GetByIdAsync(categoryId);
        var okResult = result.Result as OkObjectResult;

        // Assert
        okResult.Should().NotBeNull();
        okResult!.StatusCode.Should().Be(((int)HttpStatusCode.OK));
        okResult!.Value.Should().BeAssignableTo<ProductCategoryDto>();
        okResult!.Value.Should().Be(categoryDto);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNotFound_WhenCategoryDoesNotExist()
    {
        ProductCategoryDto? categoryDto = null;

        _mockCategoryService.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(categoryDto);

        // Act
        var result = await _controller.GetByIdAsync(_fixture.Create<int>());

        // Assert
        result.Result.Should().BeOfType<NotFoundObjectResult>();
    }
}