using SwaOlova.Application.Features.Orders.Dtos;

namespace SwaOlova.Application.Features.Orders.Commands.ConfirmOrder;

public sealed record ConfirmOrderResponse(OrderDto Order);
