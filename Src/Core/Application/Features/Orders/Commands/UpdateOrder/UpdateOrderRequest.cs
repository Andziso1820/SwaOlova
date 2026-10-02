using SwaOlova.Domain.Enums;

namespace SwaOlova.Application.Features.Orders.Commands.UpdateOrder;

public sealed record UpdateOrderRequest(
    Guid DeliveryAddressId,
    OrderType OrderType,
    decimal DeliveryFee);
