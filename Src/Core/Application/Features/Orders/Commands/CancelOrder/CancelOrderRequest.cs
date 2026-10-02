namespace SwaOlova.Application.Features.Orders.Commands.CancelOrder;

public sealed record CancelOrderRequest(Guid OrderId, string? Notes = null);
