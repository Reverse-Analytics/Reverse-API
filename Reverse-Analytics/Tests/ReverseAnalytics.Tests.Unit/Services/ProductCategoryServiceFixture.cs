namespace ReverseAnalytics.Tests.Unit.Services;

public class ProductCategoryServiceFixture : ServiceFixtureBase
{
    protected readonly ProductCategoryService _productCategoryService;

    public ProductCategoryServiceFixture()
        : base()
    {
        _productCategoryService = new ProductCategoryService(_mockRepository.Object, _mockMapper.Object);
        _fixture.Register(() => new PaginatedList<ProductCategory>(
            _fixture.CreateMany<ProductCategory>(5).ToList(),
            _fixture.Create<int>(), _fixture.Create<int>(),
            _fixture.Create<int>()));
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllProductCategories()
    {
        // Arrange
        var categories = _fixture.CreateMany<ProductCategory>(5);
        _mockRepository.Setup(r => r.ProductCategory.FindAllAsync()).ReturnsAsync(categories);

        var categoryDtos = _fixture.CreateMany<ProductCategoryDto>(5);
        _mockMapper.Setup(m => m.Map<IEnumerable<ProductCategoryDto>>(categories)).Returns(categoryDtos);

        // Act
        var result = await _productCategoryService.GetAllAsync();

        // Assert
        _mockRepository.Verify(x => x.ProductCategory.FindAllAsync(), Times.Once, "Repository was not called.");
        _mockMapper.Verify(x => x.Map<IEnumerable<ProductCategoryDto>>(categories), Times.Once, "Automapper was not called.");
        categoryDtos.Should().BeEquivalentTo(result);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnHandleEmptyResult()
    {
        // Arrange
        var categories = new List<ProductCategory>();
        _mockRepository.Setup(r => r.ProductCategory.FindAllAsync()).ReturnsAsync(categories);

        var categoryDtos = new List<ProductCategoryDto>();
        _mockMapper.Setup(m => m.Map<IEnumerable<ProductCategoryDto>>(categories)).Returns(categoryDtos);

        // Act
        var result = await _productCategoryService.GetAllAsync();

        // Assert
        _mockRepository.Verify(x => x.ProductCategory.FindAllAsync(), Times.Once, "Repository was not called.");
        _mockMapper.Verify(x => x.Map<IEnumerable<ProductCategoryDto>>(categories), Times.Once, "Automapper was not called.");
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllAsync_ShouldRethrowRepositoryException()
    {
        // Arrange
        _mockRepository.Setup(r => r.ProductCategory.FindAllAsync()).ThrowsAsync(new Exception("Database exception."));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _productCategoryService.GetAllAsync());
    }

    [Fact]
    public async Task GetAllAsync_ShouldRethrowMappingException()
    {
        // Arrange
        var categories = new List<ProductCategory>();
        _mockRepository.Setup(r => r.ProductCategory.FindAllAsync()).ReturnsAsync(categories);

        _mockMapper.Setup(m => m.Map<IEnumerable<ProductCategoryDto>>(categories)).Throws(new Exception("Mapping failed."));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _productCategoryService.GetAllAsync());
    }

    [Fact]
    public async Task GetAllAsync_ShouldThrowArgumentNullException_WhenPassedNull()
    {
        ProductCategoryQueryParameters? request = default;

        await Assert.ThrowsAsync<ArgumentNullException>(async () => await _productCategoryService.GetAllAsync(request!));
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnPaginatedListWithPaginationMetaData()
    {
        // Arrange
        var request = _fixture.Create<ProductCategoryQueryParameters>();
        var paginatedCategories = _fixture.Create<PaginatedList<ProductCategory>>();
        var metadata = paginatedCategories.MetaData;
        var categoryDtos = _fixture.CreateMany<ProductCategoryDto>(5);

        _mockRepository.Setup(x => x.ProductCategory.FindAllAsync(request)).ReturnsAsync(paginatedCategories);
        _mockMapper.Setup(x => x.Map<IEnumerable<ProductCategoryDto>>(paginatedCategories)).Returns(categoryDtos);

        // Act
        var result = await _productCategoryService.GetAllAsync(request);

        // Assert
        _mockRepository.Verify(x => x.ProductCategory.FindAllAsync(request), Times.Once);
        _mockMapper.Verify(x => x.Map<IEnumerable<ProductCategoryDto>>(paginatedCategories), Times.Once);

        result.Data.Should().BeEquivalentTo(categoryDtos);
        result.PaginationMetaData.Should().BeEquivalentTo(metadata);
    }

    [Fact]
    public async Task GetAllByParentIdAsync_ShouldReturnAllMatchingCategories()
    {
        // Arrange
        var categories = _fixture.CreateMany<ProductCategory>(5).ToList();
        var categoryDtos = _fixture.CreateMany<ProductCategoryDto>(5).ToList();
        var parentId = _fixture.Create<int>();

        _mockRepository.Setup(x => x.ProductCategory.FindByParentIdAsync(parentId)).ReturnsAsync(categories);
        _mockMapper.Setup(x => x.Map<IEnumerable<ProductCategoryDto>>(categories)).Returns(categoryDtos);

        // Act
        var result = await _productCategoryService.GetAllByParentIdAsync(parentId);

        // Assert
        result.Should().BeEquivalentTo(categoryDtos);
        _mockRepository.Verify(x => x.ProductCategory.FindByParentIdAsync(parentId), Times.Once);
        _mockMapper.Verify(x => x.Map<IEnumerable<ProductCategoryDto>>(categories), Times.Once);
    }

    [Fact]
    public async Task GetAllByParentIdAsync_ShouldReturnEmptyResult_WhenNoMatchingFound()
    {
        List<ProductCategory> categories = [];
        List<ProductCategoryDto> categoryDtos = [];
        var parentId = _fixture.Create<int>();

        _mockRepository.Setup(x => x.ProductCategory.FindByParentIdAsync(parentId)).ReturnsAsync(categories);
        _mockMapper.Setup(x => x.Map<IEnumerable<ProductCategoryDto>>(categories)).Returns(categoryDtos);

        // Act
        var result = await _productCategoryService.GetAllByParentIdAsync(parentId);

        // Assert
        result.Should().BeEquivalentTo(categoryDtos);
        _mockRepository.Verify(x => x.ProductCategory.FindByParentIdAsync(parentId), Times.Once);
        _mockMapper.Verify(x => x.Map<IEnumerable<ProductCategoryDto>>(categories), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrowNotFoundException_WhenCategoryIsNotFound()
    {
        // Arrange
        var categoryId = _fixture.Create<int>();

        // Act
        _mockRepository.Setup(x => x.ProductCategory.FindByIdAsync(categoryId)).ThrowsAsync(new EntityNotFoundException());

        // Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(() => _productCategoryService.GetByIdAsync(categoryId));
    }

    [Fact]
    public async Task GetById_ShouldReturnMatchingCategory()
    {
        // Arrange
        var categoryId = _fixture.Create<int>();
        var category = _fixture.Build<ProductCategory>().With(x => x.Id, categoryId).Create();
        var categoryDto = _fixture.Create<ProductCategoryDto>();

        _mockRepository.Setup(x => x.ProductCategory.FindByIdAsync(categoryId)).ReturnsAsync(category);
        _mockMapper.Setup(x => x.Map<ProductCategoryDto>(category)).Returns(categoryDto);

        // Act
        var result = await _productCategoryService.GetByIdAsync(categoryId);

        // Assert
        result.Should().BeEquivalentTo(categoryDto);
    }

    [Fact]
    public async Task CreateAsync_ShouldThrowArgumenNullException_WhenPassedNull()
    {
        // Arrange
        ProductCategoryForCreateDto? request = null;

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => _productCategoryService.CreateAsync(request!));
        exception.ParamName.Should().Be("categoryToCreate");
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnCreatedRecord()
    {
        // Arrange
        var request = _fixture.Create<ProductCategoryForCreateDto>();
        var category = _fixture.Create<ProductCategory>();
        var categoryDto = _fixture.Create<ProductCategoryDto>();

        _mockMapper.Setup(x => x.Map<ProductCategory>(request)).Returns(category);
        _mockRepository.Setup(x => x.ProductCategory.CreateAsync(category)).ReturnsAsync(category);
        _mockMapper.Setup(x => x.Map<ProductCategoryDto>(category)).Returns(categoryDto);

        // Act
        var result = await _productCategoryService.CreateAsync(request);

        // Assert
        result.Should().BeEquivalentTo(categoryDto);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowArgumentNullException_WhenPassedNull()
    {
        // Arrange
        ProductCategoryForUpdateDto? request = null;

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => _productCategoryService.UpdateAsync(request!));
        exception.ParamName.Should().Be("categoryToUpdate");
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnUpdatedRecord()
    {
        // Arrange
        var request = _fixture.Create<ProductCategoryForUpdateDto>();
        var category = _fixture.Create<ProductCategory>();
        var categoryDto = _fixture.Create<ProductCategoryDto>();

        _mockMapper.Setup(x => x.Map<ProductCategory>(request)).Returns(category);
        _mockRepository.Setup(x => x.ProductCategory.UpdateAsync(category)).ReturnsAsync(category);
        _mockMapper.Setup(x => x.Map<ProductCategoryDto>(category)).Returns(categoryDto);

        // Act
        var result = await _productCategoryService.UpdateAsync(request);

        // Assert
        result.Should().BeEquivalentTo(categoryDto);
    }

    [Fact]
    public async Task DeleteAsync_ShouldCallRepositoryOnce()
    {
        // Arrange
        var categoryId = _fixture.Create<int>();
        _mockRepository.Setup(x => x.ProductCategory.DeleteAsync(categoryId));

        // Act
        await _productCategoryService.DeleteAsync(categoryId);

        // Assert
        _mockRepository.Verify(x => x.ProductCategory.DeleteAsync(categoryId), Times.Once);
    }
}