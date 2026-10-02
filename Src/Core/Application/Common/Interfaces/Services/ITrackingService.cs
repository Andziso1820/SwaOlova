namespace SwaOlova.Application.Common.Interfaces.Services;

public interface ITrackingService
{
    Task<(decimal DistanceKm, int EstimatedDurationMinutes)> GetRouteMetricsAsync(
        Guid originLocationId,
        Guid destinationLocationId,
        CancellationToken cancellationToken = default);
}
