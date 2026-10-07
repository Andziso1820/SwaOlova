using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Reporting.Dtos;

namespace SwaOlova.Application.Features.Reporting.Queries.GetMerchantPerformance;

public sealed class GetMerchantPerformanceQueryHandler(
    IMerchantRepository merchantRepository,
    IOrderRepository orderRepository)
    : IRequestHandler<GetMerchantPerformanceQuery, Result<GetMerchantPerformanceResponse>>
{
    public async Task<Result<GetMerchantPerformanceResponse>> Handle(GetMerchantPerformanceQuery request, CancellationToken cancellationToken)
    {
        // Placeholder: Aggregate merchant performance metrics
        var metrics = new List<PerformanceMetricDto>
        {
            new PerformanceMetricDto(
                EntityName: "Top Merchant",
                SuccessRate: 98.5m,
                AverageRating: 4.8m,
                CompletedTransactions: 5000)
        };

        var response = new GetMerchantPerformanceResponse(metrics.AsReadOnly());
        return await Task.FromResult(Result<GetMerchantPerformanceResponse>.Success(response));
    }
}
