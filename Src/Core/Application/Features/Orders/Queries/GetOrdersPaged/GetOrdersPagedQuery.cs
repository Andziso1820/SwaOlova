using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Orders.Queries.GetOrdersPaged;

public sealed record GetOrdersPagedQuery(GetOrdersPagedRequest Request)
    : QueryBase<GetOrdersPagedResponse>;
