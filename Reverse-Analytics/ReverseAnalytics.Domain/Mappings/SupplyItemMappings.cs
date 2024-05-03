using AutoMapper;
using ReverseAnalytics.Domain.DTOs.SupplyItem;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Mappings;

public class SupplyItemMappings : Profile
{
    public SupplyItemMappings()
    {
        CreateMap<SupplyItem, SupplyItemDto>();
        CreateMap<SupplyItemForCreateDto, SupplyItem>();
        CreateMap<SupplyItemForUpdateDto, SupplyItem>();
    }
}
