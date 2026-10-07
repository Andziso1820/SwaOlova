using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Customers.Queries.GetCustomerById;

public sealed record GetCustomerByIdQuery(Guid CustomerId)
    : QueryBase<GetCustomerByIdResponse>;
