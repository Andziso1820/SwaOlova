using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Reporting.Dtos;

namespace SwaOlova.Application.Features.Reporting.Queries.GetDeliveryMetrics;

public sealed class GetDeliveryMetricsQueryHandler(
    IDeliveryRepository deliveryRepository)
    : IRequestHandler<GetDeliveryMetricsQuery, Result<DeliveryMetricsDto>>
{
    public async Task<Result<DeliveryMetricsDto>> Handle(GetDeliveryMetricsQuery request, CancellationToken cancellationToken)
    {
        // Placeholder: Calculate delivery metrics for date range
        var dto = new DeliveryMetricsDto(
            TotalDeliveries: 2000,
            OnTimeDeliveries: 1900,
            LateDeliveries: 100,
            AverageDeliveryTime: 32.5m,
            OnTimePercentage: 95.0m);

        return await Task.FromResult(Result<DeliveryMetricsDto>.Success(dto));
    }
}
