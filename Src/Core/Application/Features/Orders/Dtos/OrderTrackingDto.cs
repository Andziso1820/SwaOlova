using SwaOlova.Domain.Enums;

namespace SwaOlova.Application.Features.Orders.Dtos;

public sealed record OrderTrackingDto(
    Guid OrderId,
    string OrderNumber,
    OrderStatus OrderStatus,
    DeliveryStatus? DeliveryStatus,
    Guid? RiderId,
    string? RiderName,
    string? RiderPhoneNumber,
    DateTime? PickupTime,
    DateTime? DeliveryTime,
    decimal DistanceKm,
    decimal EstimatedDurationMinutes,
    Guid MerchantId,
    string MerchantName,
    Guid CustomerId,
    string CustomerName,
    decimal Total);
