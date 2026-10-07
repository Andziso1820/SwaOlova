namespace SwaOlova.Application.Features.Riders.Queries.GetRiderDeliveries;

public sealed record DeliverySummaryDto(
    Guid OrderId,
    string OrderNumber,
    string Status,
    decimal Distance,
    decimal EstimatedDuration,
    DateTime? PickupTime,
    DateTime? DeliveryTime);

public sealed record GetRiderDeliveriesResponse(
    IReadOnlyCollection<DeliverySummaryDto> Deliveries,
    int PageNumber,
    int PageSize,
    int TotalCount,
    int TotalPages);
