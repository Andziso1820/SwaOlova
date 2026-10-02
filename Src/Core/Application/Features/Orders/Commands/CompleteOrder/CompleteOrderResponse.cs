using SwaOlova.Application.Features.Orders.Dtos;

namespace SwaOlova.Application.Features.Orders.Commands.CompleteOrder;

public sealed record CompleteOrderResponse(OrderDto Order);
