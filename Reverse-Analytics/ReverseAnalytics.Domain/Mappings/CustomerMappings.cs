using AutoMapper;
using ReverseAnalytics.Domain.DTOs.Customer;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Mappings;

public class CustomerMappings : Profile
{
    public CustomerMappings()
    {
        CreateMap<Customer, CustomerDto>()
            .ForMember(x => x.FullName, e => e.MapFrom(s => s.FirstName + " " + s.LastName));
        CreateMap<CustomerForCreateDto, Customer>();
        CreateMap<CustomerForUpdateDto, Customer>();
    }
}
