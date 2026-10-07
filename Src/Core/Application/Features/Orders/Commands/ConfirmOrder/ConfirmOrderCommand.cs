using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Orders.Commands.ConfirmOrder;

public sealed record ConfirmOrderCommand(Guid OrderId)
    : CommandBase<ConfirmOrderResponse>;
