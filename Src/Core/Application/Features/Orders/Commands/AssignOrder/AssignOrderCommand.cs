using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Orders.Commands.AssignOrder;

public sealed record AssignOrderCommand(Guid OrderId, AssignOrderRequest Request)
    : CommandBase<AssignOrderResponse>;
