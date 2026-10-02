using SwaOlova.Application.Features.Customers.Dtos;

namespace SwaOlova.Application.Features.Customers.Queries.GetCustomerAddresses;

public sealed record GetCustomerAddressesResponse(Guid CustomerId, IReadOnlyCollection<CustomerAddressDto> Addresses);
