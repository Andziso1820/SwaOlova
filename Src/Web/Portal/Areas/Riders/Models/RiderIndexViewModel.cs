using SwaOlova.Application.Features.Riders.Queries.SearchRiders;

namespace SwaOlova.Portal.Areas.Riders.Models;

public sealed class RiderIndexViewModel
{
    public string ActiveView { get; init; } = "all";

    public string SearchTerm { get; init; } = string.Empty;

    public string Heading { get; init; } = "Manage Riders";

    public IReadOnlyCollection<SearchRiderResult> Riders { get; init; } = [];

    public IReadOnlyDictionary<string, int> DashboardCounts { get; init; } = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

    public int TotalCount => DashboardCounts.TryGetValue("all", out var count) ? count : Riders.Count;

    public static string BadgeClass(string? status) => status?.ToLowerInvariant() switch
    {
        "pendingapproval" => "bg-warning text-dark",
        "pending" => "bg-warning text-dark",
        "approved" => "bg-success",
        "active" => "bg-success",
        "available" => "bg-success",
        "busy" => "bg-info",
        "offline" => "bg-secondary",
        "suspended" => "bg-danger",
        _ => "bg-secondary"
    };
}
