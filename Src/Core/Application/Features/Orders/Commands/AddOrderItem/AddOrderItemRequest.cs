namespace SwaOlova.Application.Features.Orders.Commands.AddOrderItem;

public sealed record AddOrderItemRequest(Guid ProductId, int Quantity);
