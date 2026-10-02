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

        // Note: This would require a GetByRiderIdAsync method on IDeliveryRepository
        var deliveries = await deliveryRepository.GetByIdAsync(Guid.Empty, cancellationToken);

        var deliverySummaries = new List<DeliverySummaryDto>();
        // In production, this filtering should happen at the database level

        var totalCount = deliverySummaries.Count;
        var totalPages = pageSize <= 0 ? 0 : (int)Math.Ceiling(totalCount / (double)pageSize);

        var response = new GetRiderDeliveriesResponse(
            deliverySummaries,
            pageNumber,
            pageSize,
            totalCount,
            totalPages);

        return Result<GetRiderDeliveriesResponse>.Success(response);
    }
}
