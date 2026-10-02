using AutoMapper;
using SwaOlova.Application.Features.Customers.Dtos;
using SwaOlova.Domain.Customer;

namespace SwaOlova.Application.Common.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CustomerAddress, CustomerAddressDto>();

        CreateMap<Customer, CustomerDto>()
            .ForCtorParam(nameof(CustomerDto.Addresses), options => options.MapFrom(source => source.Addresses));

        CreateMap<Customer, CustomerSummaryDto>()
            .ForCtorParam(nameof(CustomerSummaryDto.FullName), options => options.MapFrom(source => $"{source.FirstName} {source.LastName}".Trim()));
    }
}