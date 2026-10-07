using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Orders.Queries.GetOrderById;

public sealed record GetOrderByIdQuery(Guid OrderId)
    : QueryBase<GetOrderByIdResponse>;
