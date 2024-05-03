using AutoMapper;
using ReverseAnalytics.Domain.DTOs.Supplier;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Mappings;

public class SupplierMappings : Profile
{
    public SupplierMappings()
    {
        CreateMap<Supplier, SupplierDto>()
            .ForMember(x => x.FullName, e => e.MapFrom(s => s.FirstName + " " + s.LastName));
        CreateMap<SupplierForCreateDto, Supplier>();
        CreateMap<SupplierForUpdateDto, Supplier>();
    }
}
