using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Orders.Queries.SearchOrders;

public sealed record SearchOrdersQuery(SearchOrdersRequest Request)
    : QueryBase<SearchOrdersResponse>;
