using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Orders.Queries.GetOrders;

public sealed record GetOrdersQuery(Guid CustomerId)
    : QueryBase<GetOrdersResponse>;
