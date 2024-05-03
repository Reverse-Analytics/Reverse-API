using FluentValidation.Results;
using Microsoft.AspNetCore.JsonPatch;
using ReverseAnalytics.Domain.DTOs.Product;

namespace ReverseAnalytics.Tests.Unit.Controllers;

public class ProductCategoryControllerTests : ControllerFixtureBase
{
    private readonly Mock<IProductCategoryService> _mockCategoryService;
    private readonly Mock<IProductService> _mockProductService;
    private readonly Mock<IValidator<ProductCategoryForUpdateDto>> _mockValidator;
    private readonly Mock<JsonPatchDocument<ProductCategoryForUpdateDto>> _mockPatchDocument;
    private readonly ProductCategoryController _controller;

    public ProductCategoryControllerTests()
        : base()
    {
        _mockCategoryService = new Mock<IProductCategoryService>();
        _mockProductService = new Mock<IProductService>();
        _mockValidator = new Mock<IValidator<ProductCategoryForUpdateDto>>();
        _mockPatchDocument = new Mock<JsonPatchDocument<ProductCategoryForUpdateDto>>();

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
        okResult!.StatusCode.Should().Be((int)HttpStatusCode.OK);
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

    [Fact]
    public async Task GetProductsAsync_ShouldReturnProducts()
    {
        // Arrange
        var categoryId = _fixture.Create<int>();
        var products = _fixture.CreateMany<ProductDto>(10);

        _mockProductService.Setup(x => x.GetByCategoryAsync(categoryId)).ReturnsAsync(products);

        // Act
        var result = await _controller.GetProductsAsync(categoryId);
        var okResult = result.Result as OkObjectResult;

        // Assert
        okResult.Should().NotBeNull();
        okResult!.StatusCode.Should().Be((int)HttpStatusCode.OK);
        okResult!.Value.Should().BeAssignableTo<IEnumerable<ProductDto>>();
        okResult!.Value.Should().BeEquivalentTo(products);
    }

    [Fact]
    public async Task GetChildrenAsync_ShouldReturnCategories()
    {
        // Arrange
        var parentCategoryId = _fixture.Create<int>();
        var childrenCategories = _fixture.CreateMany<ProductCategoryDto>(5);

        _mockCategoryService.Setup(x => x.GetAllByParentIdAsync(parentCategoryId)).ReturnsAsync(childrenCategories);

        // Act
        var result = await _controller.GetChildrenAsync(parentCategoryId);
        var okResult = result.Result as OkObjectResult;

        // Assert
        okResult.Should().NotBeNull();
        okResult!.StatusCode.Should().Be((int)HttpStatusCode.OK);
        okResult!.Value.Should().BeAssignableTo<IEnumerable<ProductCategoryDto>>();
        okResult!.Value.Should().BeEquivalentTo(childrenCategories);
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnCreatedCategory()
    {
        // Arrange
        var categoryToReturn = _fixture.Create<ProductCategoryDto>();
        var categoryToCreate = _fixture.Create<ProductCategoryForCreateDto>();

        _mockCategoryService.Setup(x => x.CreateAsync(categoryToCreate)).ReturnsAsync(categoryToReturn);

        // Act
        var result = await _controller.CreateAsync(categoryToCreate);
        var okResult = result.Result as OkObjectResult;

        // Assert
        okResult.Should().NotBeNull();
        okResult!.StatusCode.Should().Be((int)HttpStatusCode.OK);
        okResult!.Value.Should().BeAssignableTo<ProductCategoryDto>();
        okResult!.Value.Should().BeEquivalentTo(categoryToReturn);
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnBadRequest_WhenPassedInvalidRoutePath()
    {
        // Arrange
        var categoryToUpdate = _fixture.Create<ProductCategoryForUpdateDto>();
        var routeId = categoryToUpdate.Id + _fixture.Create<int>();

        // Act
        var result = await _controller.UpdateAsync(routeId, categoryToUpdate);
        var badRequestResult = result.Result as BadRequestObjectResult;

        // Assert
        badRequestResult.Should().NotBeNull();
        badRequestResult!.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        badRequestResult!.Value.Should().Be($"Route id: {routeId} does not match with category id: {categoryToUpdate.Id}.");
    }

    [Fact]
    public async Task UpdateAsync_SHouldReturnUpdatedCategory()
    {
        // Arrange
        var categoryToUpdate = _fixture.Create<ProductCategoryForUpdateDto>();
        var categoryToReturn = _fixture.Create<ProductCategoryDto>();

        _mockCategoryService.Setup(x => x.UpdateAsync(categoryToUpdate)).ReturnsAsync(categoryToReturn);

        // Act
        var result = await _controller.UpdateAsync(categoryToUpdate.Id, categoryToUpdate);
        var okResult = result.Result as OkObjectResult;

        // Assert
        okResult.Should().NotBeNull();
        okResult!.StatusCode.Should().Be((int)HttpStatusCode.OK);
        okResult!.Value.Should().BeAssignableTo<ProductCategoryDto>();
        okResult!.Value.Should().BeEquivalentTo(categoryToReturn);
    }

    [Fact]
    public async Task PatchAsync_ShouldReturnNotFound_WhenCategoryNotFound()
    {
        // Arrange
        int id = _fixture.Create<int>();
        ProductCategoryDto? dto = null;

        _mockCategoryService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(dto);

        // Act
        var result = await _controller.PatchAsync(id, _mockPatchDocument.Object);
        var notFoundResult = result as NotFoundObjectResult;

        // Assert
        notFoundResult.Should().NotBeNull();
        notFoundResult!.Value.Should().Be($"Category with id: {id} does not exist.");
    }

    [Fact]
    public async Task PatchAsync_ShouldReturnBadRequest_WhenValidationFails()
    {
        // Arrange
        int id = _fixture.Create<int>();
        var categoryToUpdate = _fixture.Create<ProductCategoryDto>();

        _mockCategoryService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(categoryToUpdate);
        _mockValidator.Setup(x => x.Validate(It.IsAny<ProductCategoryForUpdateDto>()))
            .Returns(new ValidationResult(new List<ValidationFailure> { new("Name", "Name is required.") }));

        // Act
        var result = await _controller.PatchAsync(id, _mockPatchDocument.Object);
        var badRequestResult = result as BadRequestObjectResult;

        // Assert
        badRequestResult.Should().NotBeNull();
        badRequestResult!.Value.Should().BeOfType<SerializableError>();

        var errors = badRequestResult!.Value as SerializableError;
        errors!.Should().ContainKey("Name");
    }

    [Fact]
    public async Task PatchAsync_ShouldReturnNoContent_WhenPatchAppliedSuccessfully()
    {
        // Arrange
        int id = _fixture.Create<int>();
        var categoryToUpdate = _fixture.Create<ProductCategoryDto>();

        _mockCategoryService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(categoryToUpdate);
        _mockValidator.Setup(x => x.Validate(It.IsAny<ProductCategoryForUpdateDto>())).Returns(new ValidationResult());

        // Act
        var result = await _controller.PatchAsync(id, _mockPatchDocument.Object);
        var noContentResult = result as NoContentResult;

        // Assert
        noContentResult.Should().NotBeNull();
        noContentResult!.StatusCode.Should().Be((int)HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task DeleteAsync_ShoudlReturnNoContent()
    {
        // Assert
        var categoryId = _fixture.Create<int>();

        // Act
        var result = await _controller.DeleteAsync(categoryId);
        var noContent = result as NoContentResult;

        // Assert
        noContent.Should().NotBeNull();
        noContent!.StatusCode.Should().Be((int)HttpStatusCode.NoContent);
    }

    [Fact]
    public void GetOptions_ShouldReturnOkResult()
    {
        // Arrange
        var expectedHeaderValues = "GET,HEAD,POST,OPTIONS";

        // Act
        var result = _controller.GetOptions();
        var okResult = result as OkResult;
        var actualHeaderValue = _response.Headers["Allow"].FirstOrDefault();

        // Assert
        okResult.Should().NotBeNull();
        okResult!.StatusCode.Should().Be((int)HttpStatusCode.OK);

        actualHeaderValue.Should().BeEquivalentTo(expectedHeaderValues);
    }
}