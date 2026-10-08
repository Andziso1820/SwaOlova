using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Domain.Enums;
using SwaOlova.Domain.Rider;

namespace SwaOlova.Application.Features.Riders.Common;

internal static class RiderActivityRecorder
{
    public static Task RecordAsync(
        IRiderRepository riderRepository,
        Guid riderId,
        RiderActivityType activityType,
        string title,
        string description,
        CancellationToken cancellationToken)
    {
        return riderRepository.AddActivityAsync(new RiderActivity
        {
            Id = Guid.NewGuid(),
            RiderId = riderId,
            ActivityType = activityType,
            Title = title,
            Description = description
        }, cancellationToken);
    }

    public static async Task ChangeStatusAsync(
        IRiderRepository riderRepository,
        Rider rider,
        RiderStatus newStatus,
        string title,
        string? reason,
        CancellationToken cancellationToken)
    {
        var previousStatus = rider.Status;
        var trimmedReason = string.IsNullOrWhiteSpace(reason) ? null : reason.Trim();

        rider.Status = newStatus;
        rider.StatusReason = trimmedReason;

        await riderRepository.UpdateAsync(rider, cancellationToken);

        var description = $"Status changed from {previousStatus} to {newStatus}.";
        if (trimmedReason is not null)
        {
            description += $" Reason: {trimmedReason}";
        }

        await RecordAsync(riderRepository, rider.Id, RiderActivityType.StatusChanged, title, description, cancellationToken);
    }
}
