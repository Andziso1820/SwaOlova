namespace SwaOlova.Application.Features.Dashboard.Dtos;

public sealed record AdminDashboardDto(
    int TotalUsers,
    int ActiveMerchants,
    int ActiveRiders,
    decimal TotalRevenue,
    int ActiveOrders,
    decimal AverageOrderValue);

public sealed record DispatcherDashboardDto(
    int PendingDeliveries,
    int InProgressDeliveries,
    int CompletedDeliveries,
    int AvailableRiders,
    decimal OnTimePercentage);

public sealed record MerchantDashboardDto(
    Guid MerchantId,
    string MerchantName,
    int TotalOrders,
    int PendingOrders,
    decimal TotalRevenue,
    decimal AverageRating,
    int ActiveProducts);

public sealed record RiderDashboardDto(
    Guid RiderId,
    string RiderName,
    int CompletedDeliveries,
    int PendingDeliveries,
    decimal TotalEarnings,
    decimal AverageRating,
    decimal OnTimePercentage);
