using AutoMapper;
using ReverseAnalytics.Domain.DTOs.Sale;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Mappings;

public class SaleMappings : Profile
{
    public SaleMappings()
    {
        CreateMap<Sale, SaleDto>();
        CreateMap<SaleForCreateDto, Sale>();
        CreateMap<SaleForUpdateDto, Sale>();
    }
}
