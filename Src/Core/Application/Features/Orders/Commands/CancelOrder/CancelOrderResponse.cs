using SwaOlova.Application.Features.Orders.Dtos;

namespace SwaOlova.Application.Features.Orders.Commands.CancelOrder;

public sealed record CancelOrderResponse(OrderDto Order);
