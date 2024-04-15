using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ReverseAnalytics.Domain.DTOs.Product;
using ReverseAnalytics.Domain.DTOs.ProductCategory;
using ReverseAnalytics.Domain.Interfaces.Services;
using ReverseAnalytics.Domain.QueryParameters;

namespace Reverse_Analytics.Api.Controllers;

[Route("api/categories")]
[ApiController]
public class ProductCategoryController(
    IProductService productService,
    IProductCategoryService productCategoryService,
    IValidator<ProductCategoryForUpdateDto> validator) : CommonControllerBase
{
    private readonly IProductService _productService = productService ?? throw new ArgumentNullException(nameof(productService));
    private readonly IProductCategoryService _productCategoryService = productCategoryService ?? throw new ArgumentNullException(nameof(productCategoryService));
    private readonly IValidator<ProductCategoryForUpdateDto> _validator = validator ?? throw new ArgumentNullException(nameof(validator));

    [HttpGet]
    [HttpHead]
    public async Task<ActionResult<IEnumerable<ProductCategoryDto>>> GetCategoriesAsync([FromQuery] ProductCategoryQueryParameters queryParameters)
    {
        var (categories, metadata) = await _productCategoryService.GetAllAsync(queryParameters);
        var json = JsonConvert.SerializeObject(metadata, Formatting.Indented);
        Response.Headers.Append("X-Pagination", json);

        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductCategoryDto>> GetByIdAsync(int id)
    {
        var category = await _productCategoryService.GetByIdAsync(id);

        if (category is null)
        {
            return NotFound($"Category with id: {id} does not exist.");
        }

        return Ok(category);
    }

    [HttpGet("{id}/products")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsAsync(int id)
    {
        var products = await _productService.GetByCategoryAsync(id);

        return Ok(products);
    }

    [HttpGet("{id}/children")]
    public async Task<ActionResult<IEnumerable<ProductCategoryDto>>> GetChildrenAsync(int id)
    {
        var subCategories = await _productCategoryService.GetAllByParentIdAsync(id);

        return Ok(subCategories);
    }

    [HttpPost]
    public async Task<ActionResult<ProductCategoryDto>> CreateAsync([FromBody] ProductCategoryForCreateDto categoryToCreate)
    {
        var createdCategory = await _productCategoryService.CreateAsync(categoryToCreate);

        return Ok(createdCategory);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProductCategoryDto>> UpdateAsync(int id, [FromBody] ProductCategoryForUpdateDto categoryToUpdate)
    {
        if (id != categoryToUpdate.Id)
        {
            return BadRequest($"Route id: {id} does not match with category id: {categoryToUpdate.Id}.");
        }

        var updatedCategory = await _productCategoryService.UpdateAsync(categoryToUpdate);

        return Ok(updatedCategory);
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult> PatchAsync(int id, JsonPatchDocument<ProductCategoryForUpdateDto> patchDocument)
    {
        var categoryToUpdate = await _productCategoryService.GetByIdAsync(id);

        if (categoryToUpdate is null)
        {
            return NotFound($"Category with id: {id} does not exist.");
        }

        var categoryDto = new ProductCategoryForUpdateDto(id, categoryToUpdate.Name, categoryToUpdate.Description, categoryToUpdate.ParentId);

        patchDocument.ApplyTo(categoryDto);

        var result = _validator.Validate(categoryDto);

        if (!result.IsValid)
        {
            result.AddToModelState(ModelState);

            return BadRequest(ModelState);
        }

        await _productCategoryService.UpdateAsync(categoryDto);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        await _productCategoryService.DeleteAsync(id);

        return NoContent();
    }

    [HttpOptions]
    public IActionResult GetOptions()
    {
        Response.Headers.Append("Allow", "GET,HEAD,POST,OPTIONS");
        return Ok();
    }
}
