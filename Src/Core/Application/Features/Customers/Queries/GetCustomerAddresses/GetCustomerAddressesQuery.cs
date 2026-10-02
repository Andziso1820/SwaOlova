using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Customers.Queries.GetCustomerAddresses;

public sealed record GetCustomerAddressesQuery(Guid CustomerId)
    : QueryBase<GetCustomerAddressesResponse>;
