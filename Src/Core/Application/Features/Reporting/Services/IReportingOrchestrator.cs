using MediatR;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Reporting.Dtos;
using SwaOlova.Application.Features.Reporting.Queries.GetDashboardStatistics;
using SwaOlova.Application.Features.Reporting.Queries.GetDeliveryMetrics;
using SwaOlova.Application.Features.Reporting.Queries.GetFinancialSummary;
using SwaOlova.Application.Features.Reporting.Queries.GetMerchantPerformance;
using SwaOlova.Application.Features.Reporting.Queries.GetOrderReport;
using SwaOlova.Application.Features.Reporting.Queries.GetRevenueReport;
using SwaOlova.Application.Features.Reporting.Queries.GetRiderPerformance;

namespace SwaOlova.Application.Features.Reporting.Services;

public interface IReportingOrchestrator
{
    Task<Result<DashboardStatisticsDto>> GetDashboardStatisticsAsync(CancellationToken cancellationToken = default);
    Task<Result<GetRevenueReportResponse>> GetRevenueReportAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
    Task<Result<OrderReportDto>> GetOrderReportAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
    Task<Result<GetMerchantPerformanceResponse>> GetMerchantPerformanceAsync(CancellationToken cancellationToken = default);
    Task<Result<GetRiderPerformanceResponse>> GetRiderPerformanceAsync(CancellationToken cancellationToken = default);
    Task<Result<DeliveryMetricsDto>> GetDeliveryMetricsAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
    Task<Result<FinancialSummaryDto>> GetFinancialSummaryAsync(CancellationToken cancellationToken = default);
}
