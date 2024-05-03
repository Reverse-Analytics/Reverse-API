using AutoMapper;
using ReverseAnalytics.Domain.DTOs.Sale;
using ReverseAnalytics.Domain.DTOs.Supply;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Mappings;

public class SupplyMappings : Profile
{
    public SupplyMappings()
    {
        CreateMap<Supply, SupplyDto>();
        CreateMap<SupplyForCreateDto, Supply>();
        CreateMap<SupplyForUpdateDto, Supply>();
    }
}
