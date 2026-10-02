namespace SwaOlova.Infrastructure.Service.Reporting;

public interface IDashboardService
{
    Task<IReadOnlyDictionary<string, object?>> GetAdminDashboardAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyDictionary<string, object?>> GetDispatcherDashboardAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyDictionary<string, object?>> GetMerchantDashboardAsync(Guid merchantId, CancellationToken cancellationToken = default);

    Task<IReadOnlyDictionary<string, object?>> GetRiderDashboardAsync(Guid riderId, CancellationToken cancellationToken = default);
}