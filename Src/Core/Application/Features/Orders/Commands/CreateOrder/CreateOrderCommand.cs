using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Orders.Commands.CreateOrder;

public sealed record CreateOrderCommand(CreateOrderRequest Request)
    : CommandBase<CreateOrderResponse>;
