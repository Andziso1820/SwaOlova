using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Reporting.Dtos;

namespace SwaOlova.Application.Features.Reporting.Queries.GetRiderPerformance;

public sealed class GetRiderPerformanceQueryHandler(
    IRiderRepository riderRepository,
    IDeliveryRepository deliveryRepository)
    : IRequestHandler<GetRiderPerformanceQuery, Result<GetRiderPerformanceResponse>>
{
    public async Task<Result<GetRiderPerformanceResponse>> Handle(GetRiderPerformanceQuery request, CancellationToken cancellationToken)
    {
        // Placeholder: Aggregate rider performance metrics
        var metrics = new List<SwaOlova.Application.Features.Reporting.Dtos.PerformanceMetricDto>
        {
            new SwaOlova.Application.Features.Reporting.Dtos.PerformanceMetricDto(
                EntityName: "Top Rider",
                SuccessRate: 99.2m,
                AverageRating: 4.9m,
                CompletedTransactions: 3500)
        };

        var response = new GetRiderPerformanceResponse(metrics.AsReadOnly());
        return await Task.FromResult(Result<GetRiderPerformanceResponse>.Success(response));
    }
}
