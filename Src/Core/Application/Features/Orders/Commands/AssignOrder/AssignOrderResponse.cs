using SwaOlova.Application.Features.Orders.Dtos;

namespace SwaOlova.Application.Features.Orders.Commands.AssignOrder;

public sealed record AssignOrderResponse(OrderDto Order, Guid RiderId);
