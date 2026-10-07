using SwaOlova.Domain.Enums;

namespace SwaOlova.Application.Features.Orders.Dtos;

public sealed record OrderDto(
    Guid Id,
    string OrderNumber,
    Guid CustomerId,
    string? CustomerName,
    string? CustomerPhone,
    Guid MerchantId,
    Guid DeliveryAddressId,
    string? DeliveryAddress,
    string? SpecialInstructions,
    OrderType OrderType,
    OrderStatus Status,
    decimal SubTotal,
    decimal DeliveryFee,
    decimal Total,
    decimal DiscountAmount,
    DateTime? CreatedDate,
    DateTime? UpdatedDate,
    IReadOnlyCollection<OrderItemDto> Items);
