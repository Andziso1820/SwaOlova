using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Orders.Queries.GetOrderTracking;

public sealed record GetOrderTrackingQuery(Guid OrderId)
    : QueryBase<GetOrderTrackingResponse>;
