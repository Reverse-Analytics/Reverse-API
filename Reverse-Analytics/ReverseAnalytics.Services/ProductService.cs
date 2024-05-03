using AutoMapper;
using ReverseAnalytics.Domain.DTOs.Product;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Domain.Interfaces.Services;
using ReverseAnalytics.Domain.QueryParameters;

namespace ReverseAnalytics.Services;

public sealed class ProductService(ICommonRepository repository, IMapper mapper) : IProductService
{
    private readonly ICommonRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        var entities = await _repository.Product.FindAllAsync();

        return _mapper.Map<IEnumerable<ProductDto>>(entities);
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync(ProductQueryParameters queryParameters)
    {
        var entities = await _repository.Product.FindAllAsync(queryParameters);

        return _mapper.Map<IEnumerable<ProductDto>>(entities);
    }

    public async Task<IEnumerable<ProductDto>> GetByCategoryAsync(int categoryId)
    {
        var entities = await _repository.Product.FindByCategoryIdAsync(categoryId);

        return _mapper.Map<IEnumerable<ProductDto>>(entities);
    }

    public async Task<ProductDto> GetByIdAsync(int id)
    {
        var entity = await _repository.Product.FindByIdAsync(id);

        return _mapper.Map<ProductDto>(entity);
    }

    public async Task<ProductDto> CreateAsync(ProductForCreateDto productToCreate)
    {
        var entity = _mapper.Map<Product>(productToCreate);
        var createdEntity = await _repository.Product.CreateAsync(entity);

        return _mapper.Map<ProductDto>(createdEntity);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.Product.DeleteAsync(id);
    }

    public async Task<ProductDto> UpdateAsync(ProductForUpdateDto productToUpdate)
    {
        var entity = _mapper.Map<Product>(productToUpdate);
        var updatedEntity = await _repository.Product.UpdateAsync(entity);

        return _mapper.Map<ProductDto>(updatedEntity);
    }
}
