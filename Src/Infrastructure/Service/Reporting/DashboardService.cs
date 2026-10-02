namespace SwaOlova.Infrastructure.Service.Reporting;

public sealed class DashboardService : IDashboardService
{
    public Task<IReadOnlyDictionary<string, object?>> GetAdminDashboardAsync(CancellationToken cancellationToken = default)
        => Task.FromResult<IReadOnlyDictionary<string, object?>>(new Dictionary<string, object?>
        {
            ["scope"] = "admin",
            ["generatedAt"] = DateTime.UtcNow
        });

    public Task<IReadOnlyDictionary<string, object?>> GetDispatcherDashboardAsync(CancellationToken cancellationToken = default)
        => Task.FromResult<IReadOnlyDictionary<string, object?>>(new Dictionary<string, object?>
        {
            ["scope"] = "dispatcher",
            ["generatedAt"] = DateTime.UtcNow
        });

    public Task<IReadOnlyDictionary<string, object?>> GetMerchantDashboardAsync(Guid merchantId, CancellationToken cancellationToken = default)
        => Task.FromResult<IReadOnlyDictionary<string, object?>>(new Dictionary<string, object?>
        {
            ["scope"] = "merchant",
            ["merchantId"] = merchantId,
            ["generatedAt"] = DateTime.UtcNow
        });

    public Task<IReadOnlyDictionary<string, object?>> GetRiderDashboardAsync(Guid riderId, CancellationToken cancellationToken = default)
        => Task.FromResult<IReadOnlyDictionary<string, object?>>(new Dictionary<string, object?>
        {
            ["scope"] = "rider",
            ["riderId"] = riderId,
            ["generatedAt"] = DateTime.UtcNow
        });
}