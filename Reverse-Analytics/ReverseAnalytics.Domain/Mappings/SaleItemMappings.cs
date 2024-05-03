using AutoMapper;
using ReverseAnalytics.Domain.DTOs.SaleItem;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Mappings;

public class SaleItemMappings : Profile
{
    public SaleItemMappings()
    {
        CreateMap<SaleItem, SaleItemDto>();
        CreateMap<SaleItemForCreateDto, SaleItem>();
        CreateMap<SaleItemForUpdateDto, SaleItem>();
    }
}
