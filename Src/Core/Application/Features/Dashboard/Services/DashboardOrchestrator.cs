using MediatR;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Dashboard.Dtos;
using SwaOlova.Application.Features.Dashboard.Queries.GetAdminDashboard;
using SwaOlova.Application.Features.Dashboard.Queries.GetDispatcherDashboard;
using SwaOlova.Application.Features.Dashboard.Queries.GetMerchantDashboard;
using SwaOlova.Application.Features.Dashboard.Queries.GetRiderDashboard;

namespace SwaOlova.Application.Features.Dashboard.Services;

public sealed class DashboardOrchestrator(IMediator mediator)
    : IDashboardOrchestrator
{
    public async Task<Result<AdminDashboardDto>> GetAdminDashboardAsync(CancellationToken cancellationToken = default)
    {
        var query = new GetAdminDashboardQuery();
        return await mediator.Send(query, cancellationToken);
    }

    public async Task<Result<DispatcherDashboardDto>> GetDispatcherDashboardAsync(CancellationToken cancellationToken = default)
    {
        var query = new GetDispatcherDashboardQuery();
        return await mediator.Send(query, cancellationToken);
    }

    public async Task<Result<MerchantDashboardDto>> GetMerchantDashboardAsync(Guid merchantId, CancellationToken cancellationToken = default)
    {
        var query = new GetMerchantDashboardQuery(merchantId);
        return await mediator.Send(query, cancellationToken);
    }

    public async Task<Result<RiderDashboardDto>> GetRiderDashboardAsync(Guid riderId, CancellationToken cancellationToken = default)
    {
        var query = new GetRiderDashboardQuery(riderId);
        return await mediator.Send(query, cancellationToken);
    }
}
