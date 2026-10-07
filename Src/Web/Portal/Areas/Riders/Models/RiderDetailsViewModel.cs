using SwaOlova.Application.Features.Riders.Dtos;
using SwaOlova.Application.Features.Riders.Queries.GetRiderDeliveries;
using SwaOlova.Domain.Enums;

namespace SwaOlova.Portal.Areas.Riders.Models;

public sealed class RiderDetailsViewModel
{
    public RiderDto Rider { get; init; } = new(
        Guid.Empty,
        string.Empty,
        string.Empty,
        string.Empty,
        string.Empty,
        string.Empty,
        string.Empty,
        string.Empty,
        RiderStatus.PendingApproval,
        null,
        null,
        null,
        null,
        null,
        [],
        null);

    public IReadOnlyCollection<DeliverySummaryDto> Deliveries { get; init; } = [];

    public string? RiderActionMessage { get; set; }

    public string? RiderErrorMessage { get; set; }

    public string SuspendReason { get; set; } = string.Empty;

    public bool IsAvailable { get; set; }

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    public string DocumentFileName { get; set; } = string.Empty;

    public string DocumentFileUrl { get; set; } = string.Empty;

    public int TotalDeliveries => Deliveries.Count;

    public int CompletedDeliveries => Deliveries.Count(delivery => string.Equals(delivery.Status, "Completed", StringComparison.OrdinalIgnoreCase));

    public int CancelledDeliveries => Deliveries.Count(delivery =>
        string.Equals(delivery.Status, "Cancelled", StringComparison.OrdinalIgnoreCase)
        || string.Equals(delivery.Status, "Failed", StringComparison.OrdinalIgnoreCase));

    public decimal SuccessRate => TotalDeliveries == 0
        ? 0
        : Math.Round((decimal)CompletedDeliveries / TotalDeliveries * 100, 1);

    public bool CanApprove => Rider.Status == RiderStatus.PendingApproval;

    public bool CanSuspend => Rider.Status != RiderStatus.Suspended;

    public bool CanChangeAvailability => Rider.Status != RiderStatus.Suspended;

    public bool CanUpdateLocation => Rider.Status != RiderStatus.Suspended;

    public bool CanUploadDocument => true;

    public string AvailabilityLabel => IsAvailable ? "Available" : "Offline";

    public string LocationSummary => Rider.CurrentLocation is null
        ? "No live location recorded."
        : $"{Rider.CurrentLocation.Latitude:F6}, {Rider.CurrentLocation.Longitude:F6} · {Rider.CurrentLocation.RecordedAt:dd MMM yyyy HH:mm}";

    public static string BadgeClass(string? status) => RiderIndexViewModel.BadgeClass(status);
}
