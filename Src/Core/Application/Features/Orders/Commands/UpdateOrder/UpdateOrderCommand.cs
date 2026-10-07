using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Orders.Commands.UpdateOrder;

public sealed record UpdateOrderCommand(Guid OrderId, UpdateOrderRequest Request)
    : CommandBase<UpdateOrderResponse>;
