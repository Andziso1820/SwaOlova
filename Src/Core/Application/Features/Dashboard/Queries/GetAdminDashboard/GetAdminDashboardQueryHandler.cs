using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Dashboard.Dtos;

namespace SwaOlova.Application.Features.Dashboard.Queries.GetAdminDashboard;

public sealed class GetAdminDashboardQueryHandler(
    IOrderRepository orderRepository,
    IMerchantRepository merchantRepository,
    IRiderRepository riderRepository)
    : IRequestHandler<GetAdminDashboardQuery, Result<AdminDashboardDto>>
{
    public async Task<Result<AdminDashboardDto>> Handle(GetAdminDashboardQuery request, CancellationToken cancellationToken)
    {
        // Placeholder: Aggregate data for admin dashboard
        var dto = new AdminDashboardDto(
            TotalUsers: 5000,
            ActiveMerchants: 150,
            ActiveRiders: 500,
            TotalRevenue: 500000m,
            ActiveOrders: 250,
            AverageOrderValue: 3500m);

        return await Task.FromResult(Result<AdminDashboardDto>.Success(dto));
    }
}
