using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Customers.Queries.GetCustomerOrderHistory;

public sealed record GetCustomerOrderHistoryQuery(Guid CustomerId, GetCustomerOrderHistoryRequest Request)
    : QueryBase<GetCustomerOrderHistoryResponse>;
