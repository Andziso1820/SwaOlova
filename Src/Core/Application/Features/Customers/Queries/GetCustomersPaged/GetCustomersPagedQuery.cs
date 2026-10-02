using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Customers.Queries.GetCustomersPaged;

public sealed record GetCustomersPagedQuery(GetCustomersPagedRequest Request)
    : QueryBase<GetCustomersPagedResponse>;
