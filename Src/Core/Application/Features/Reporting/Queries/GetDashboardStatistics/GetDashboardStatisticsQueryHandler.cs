using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Reporting.Dtos;
using SwaOlova.Domain.Enums;

namespace SwaOlova.Application.Features.Reporting.Queries.GetDashboardStatistics;

public sealed class GetDashboardStatisticsQueryHandler(
    IOrderRepository orderRepository,
    IMerchantRepository merchantRepository,
    IRiderRepository riderRepository)
    : IRequestHandler<GetDashboardStatisticsQuery, Result<DashboardStatisticsDto>>
{
    public async Task<Result<DashboardStatisticsDto>> Handle(GetDashboardStatisticsQuery request, CancellationToken cancellationToken)
    {
        // Placeholder: In real scenario, these would be aggregated from repositories
        var dto = new DashboardStatisticsDto(
            TotalOrders: 1250,
            CompletedOrders: 1100,
            PendingOrders: 50,
            TotalRevenue: 50000m,
            ActiveMerchants: 45,
            ActiveRiders: 120);

        return await Task.FromResult(Result<DashboardStatisticsDto>.Success(dto));
    }
}
