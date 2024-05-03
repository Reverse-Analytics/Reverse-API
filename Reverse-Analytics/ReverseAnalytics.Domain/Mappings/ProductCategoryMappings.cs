using AutoMapper;
using ReverseAnalytics.Domain.DTOs.ProductCategory;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Mappings;

public class ProductCategoryMappings : Profile
{
    public ProductCategoryMappings()
    {
        CreateMap<ProductCategory, ProductCategoryDto>()
            .ForMember(d => d.ParentName, opt =>
            {
                opt.PreCondition(s => s.Parent is not null);
                opt.MapFrom(s => s.Parent!.Name);
            });
        CreateMap<ProductCategoryForCreateDto, ProductCategory>();
        CreateMap<ProductCategoryForUpdateDto, ProductCategory>();
    }
}
