using AutoMapper;
using ReverseAnalytics.Domain.DTOs.Product;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Mappings;

public class ProductMappings : Profile
{
    public ProductMappings()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<ProductForCreateDto, Product>();
        CreateMap<ProductForUpdateDto, Product>();
    }
}
