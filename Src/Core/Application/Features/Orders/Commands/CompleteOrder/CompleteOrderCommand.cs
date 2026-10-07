using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Orders.Commands.CompleteOrder;

public sealed record CompleteOrderCommand(Guid OrderId)
    : CommandBase<CompleteOrderResponse>;
