namespace SwaOlova.Application.Features.Reporting.Dtos;

public sealed record DashboardStatisticsDto(
    int TotalOrders,
    int CompletedOrders,
    int PendingOrders,
    decimal TotalRevenue,
    int ActiveMerchants,
    int ActiveRiders);

public sealed record RevenueReportDto(
    DateTime ReportDate,
    decimal TotalRevenue,
    decimal CommissionCollected,
    decimal NetRevenue,
    int OrderCount);

public sealed record OrderReportDto(
    int TotalOrders,
    int CompletedOrders,
    int CancelledOrders,
    int PendingOrders,
    decimal AverageOrderValue);

public sealed record PerformanceMetricDto(
    string EntityName,
    decimal SuccessRate,
    decimal AverageRating,
    int CompletedTransactions);

public sealed record DeliveryMetricsDto(
    int TotalDeliveries,
    int OnTimeDeliveries,
    int LateDeliveries,
    decimal AverageDeliveryTime,
    decimal OnTimePercentage);

public sealed record FinancialSummaryDto(
    decimal TotalIncome,
    decimal TotalExpenses,
    decimal NetProfit,
    decimal ProfitMargin);
