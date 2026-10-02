using MediatR;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Dashboard.Dtos;
using SwaOlova.Application.Features.Dashboard.Queries.GetAdminDashboard;
using SwaOlova.Application.Features.Dashboard.Queries.GetDispatcherDashboard;
using SwaOlova.Application.Features.Dashboard.Queries.GetMerchantDashboard;
using SwaOlova.Application.Features.Dashboard.Queries.GetRiderDashboard;

namespace SwaOlova.Application.Features.Dashboard.Services;

public interface IDashboardOrchestrator
{
    Task<Result<AdminDashboardDto>> GetAdminDashboardAsync(CancellationToken cancellationToken = default);
    Task<Result<DispatcherDashboardDto>> GetDispatcherDashboardAsync(CancellationToken cancellationToken = default);
    Task<Result<MerchantDashboardDto>> GetMerchantDashboardAsync(Guid merchantId, CancellationToken cancellationToken = default);
    Task<Result<RiderDashboardDto>> GetRiderDashboardAsync(Guid riderId, CancellationToken cancellationToken = default);
}
