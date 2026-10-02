using SwaOlova.Domain.Enums;

namespace SwaOlova.Application.Features.Orders.Commands.CreateOrder;

public sealed record CreateOrderRequest(
    Guid CustomerId,
    Guid MerchantId,
    Guid DeliveryAddressId,
    OrderType OrderType,
    decimal DeliveryFee,
    IReadOnlyCollection<CreateOrderItemRequest> Items);

public sealed record CreateOrderItemRequest(Guid ProductId, int Quantity);
