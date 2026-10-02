namespace SwaOlova.Application.Features.Orders.Commands.CompleteOrder;

public sealed record CompleteOrderRequest(Guid OrderId, string? DeliveryNotes = null);
