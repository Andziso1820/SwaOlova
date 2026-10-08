using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;

namespace SwaOlova.Application.Features.Riders.Queries.GetRiderDeliveries;

public sealed class GetRiderDeliveriesQueryHandler(
    IRiderRepository riderRepository,
    IDeliveryRepository deliveryRepository)
    : IRequestHandler<GetRiderDeliveriesQuery, Result<GetRiderDeliveriesResponse>>
{
    public async Task<Result<GetRiderDeliveriesResponse>> Handle(GetRiderDeliveriesQuery request, CancellationToken cancellationToken)
    {
        var rider = await riderRepository.GetByIdAsync(request.RiderId, cancellationToken);
        if (rider is null)
        {
            return Result<GetRiderDeliveriesResponse>.Failure($"Rider with ID '{request.RiderId}' was not found.");
        }

        var pageNumber = request.PageNumber <= 0 ? 1 : request.PageNumber;
        var pageSize = request.PageSize <= 0 ? 10 : request.PageSize;

        var rows = await deliveryRepository.GetByRiderIdAsync(rider.Id, pageNumber, pageSize, cancellationToken);
        var totalCount = await deliveryRepository.CountByRiderIdAsync(rider.Id, cancellationToken);

        var deliveries = rows
            .Select(row => new DeliverySummaryDto(
                row.Delivery.OrderId,
                row.OrderNumber,
                row.Delivery.Status.ToString(),
                row.Delivery.DistanceKm,
                row.Delivery.EstimatedDurationMinutes,
                row.Delivery.PickupTime,
                row.Delivery.DeliveryTime))
            .ToArray();

        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        return Result<GetRiderDeliveriesResponse>.Success(
            new GetRiderDeliveriesResponse(deliveries, pageNumber, pageSize, totalCount, totalPages));
    }
}
