﻿using AutoMapper;
using ReverseAnalytics.Domain.Common;
using ReverseAnalytics.Domain.DTOs.ProductCategory;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Domain.Interfaces.Services;
using ReverseAnalytics.Domain.QueryParameters;

namespace ReverseAnalytics.Services;

public class ProductCategoryService(ICommonRepository repository, IMapper mapper) : IProductCategoryService
{
    private readonly ICommonRepository _repository = repository;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<ProductCategoryDto>> GetAsync()
    {
        var entities = await _repository.ProductCategory.FindAllAsync();

        return _mapper.Map<IEnumerable<ProductCategoryDto>>(entities);
    }

    public async Task<(IEnumerable<ProductCategoryDto>, PaginationMetaData)> GetAsync(ProductCategoryQueryParameters queryParameters)
    {
        var paginatedResult = await _repository.ProductCategory.FindAllAsync(queryParameters);

        return (_mapper.Map<List<ProductCategoryDto>>(paginatedResult), paginatedResult.ToMetaData());
    }

    public async Task<IEnumerable<ProductCategoryDto>> GetAllByParentIdAsync(int parentId)
    {
        var entities = await _repository.ProductCategory.FindByParentIdAsync(parentId);

        return _mapper.Map<IEnumerable<ProductCategoryDto>>(entities);
    }

    public async Task<ProductCategoryDto> GetByIdAsync(int id)
    {
        var entity = await _repository.ProductCategory.FindByIdAsync(id);

        return _mapper.Map<ProductCategoryDto>(entity);
    }

    public async Task<ProductCategoryDto> CreateAsync(ProductCategoryForCreateDto categoryToCreate)
    {
        var entity = _mapper.Map<ProductCategory>(categoryToCreate);
        var createdEntity = await _repository.ProductCategory.CreateAsync(entity);

        return _mapper.Map<ProductCategoryDto>(createdEntity);
    }

    public async Task<ProductCategoryDto> UpdateAsync(ProductCategoryForUpdateDto categoryToUpdate)
    {
        var entity = _mapper.Map<ProductCategory>(categoryToUpdate);
        var updatedEntity = await _repository.ProductCategory.UpdateAsync(entity);

        return _mapper.Map<ProductCategoryDto>(updatedEntity);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.ProductCategory.DeleteAsync(id);
    }
}
