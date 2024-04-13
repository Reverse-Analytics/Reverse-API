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
    public async Task GetCategoriesAsync_ShouldReturnOkResult_WhenExecutedSuccessfully()
    {
        // Arrange
        var queryParameters = _fixture.Create<ProductCategoryQueryParameters>();
        var paginatedCategories = CreatePaginatedList<ProductCategoryDto>();
        var metadata = paginatedCategories.MetaData;

        _mockCategoryService.Setup(x => x.GetAllAsync(queryParameters)).ReturnsAsync((paginatedCategories, metadata));

        // Act
        var result = await _controller.GetCategoriesAsync(queryParameters);

        // Assert
        result.Should().NotBeNull();
        result.Result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GetCategories_ShouldReturn200StatusCode_WhenExecutedSuccessfully()
    {
        // Arrange
        var queryParameters = _fixture.Create<ProductCategoryQueryParameters>();
        var paginatedCategories = CreatePaginatedList<ProductCategoryDto>();
        var metadata = paginatedCategories.MetaData;

        _mockCategoryService.Setup(x => x.GetAllAsync(queryParameters)).ReturnsAsync((paginatedCategories, metadata));

        // Act
        var result = await _controller.GetCategoriesAsync(queryParameters);

        // Assert
        var okResult = result.Result as OkObjectResult;
        okResult!.StatusCode.Should().Be(((int)HttpStatusCode.OK));
    }

    [Fact]
    public async Task GetCategories_ShouldReturnCategoryDtosAsModelType_WhenCalled()
    {
        // Arrange
        var queryParameters = _fixture.Create<ProductCategoryQueryParameters>();
        var paginatedCategories = CreatePaginatedList<ProductCategoryDto>();
        var metadata = paginatedCategories.MetaData;

        _mockCategoryService.Setup(x => x.GetAllAsync(queryParameters)).ReturnsAsync((paginatedCategories, metadata));

        // Act
        var result = await _controller.GetCategoriesAsync(queryParameters);

        // Assert
        var okResult = result.Result as OkObjectResult;
        okResult!.Value.Should().BeAssignableTo<IEnumerable<ProductCategoryDto>>();
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

        // Assert
        var okResult = result.Result as OkObjectResult;
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

        // Assert
        _headerDictionary.Should().ContainKey("X-Pagination");

        var expectedHeaderValue = JsonConvert.SerializeObject(metadata, Formatting.Indented);
        _headerDictionary["X-Pagination"].Should().BeEquivalentTo(expectedHeaderValue);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnOkResult_WhenExecutedSuccessfully()
    {
        // Arrange
        var categoryId = _fixture.Create<int>();
        var categoryDto = _fixture.Build<ProductCategoryDto>()
                                  .With(x => x.Id, categoryId)
                                  .Create();

        _mockCategoryService.Setup(x => x.GetByIdAsync(categoryId)).ReturnsAsync(categoryDto);

        // Act
        var result = await _controller.GetByIdAsync(categoryId);

        // Assert
        result.Should().NotBeNull();
        result.Result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturn200StatusCode_WhenExecutedSuccessfully()
    {
        // Arrange
        var categoryId = _fixture.Create<int>();
        var categoryDto = _fixture.Build<ProductCategoryDto>()
                                  .With(x => x.Id, categoryId)
                                  .Create();

        _mockCategoryService.Setup(x => x.GetByIdAsync(categoryId)).ReturnsAsync(categoryDto);

        // Act
        var result = await _controller.GetByIdAsync(categoryId);

        // Assert
        var okResult = result.Result as OkObjectResult;
        okResult!.StatusCode.Should().Be((int)HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnCategory_WhenCalled()
    {
        // Arrange
        var categoryId = _fixture.Create<int>();
        var categoryDto = _fixture.Build<ProductCategoryDto>()
                                  .With(x => x.Id, categoryId)
                                  .Create();

        _mockCategoryService.Setup(x => x.GetByIdAsync(categoryId)).ReturnsAsync(categoryDto);

        // Act
        var result = await _controller.GetByIdAsync(categoryId);

        // Assert
        var okResult = result.Result as OkObjectResult;
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