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

public sealed class ReportingOrchestrator(IMediator mediator)
    : IReportingOrchestrator
{
    public async Task<Result<DashboardStatisticsDto>> GetDashboardStatisticsAsync(CancellationToken cancellationToken = default)
    {
        var query = new GetDashboardStatisticsQuery();
        return await mediator.Send(query, cancellationToken);
    }

    public async Task<Result<GetRevenueReportResponse>> GetRevenueReportAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
    {
        var query = new GetRevenueReportQuery(startDate, endDate);
        return await mediator.Send(query, cancellationToken);
    }

    public async Task<Result<OrderReportDto>> GetOrderReportAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
    {
        var query = new GetOrderReportQuery(startDate, endDate);
        return await mediator.Send(query, cancellationToken);
    }

    public async Task<Result<GetMerchantPerformanceResponse>> GetMerchantPerformanceAsync(CancellationToken cancellationToken = default)
    {
        var query = new GetMerchantPerformanceQuery();
        return await mediator.Send(query, cancellationToken);
    }

    public async Task<Result<GetRiderPerformanceResponse>> GetRiderPerformanceAsync(CancellationToken cancellationToken = default)
    {
        var query = new GetRiderPerformanceQuery();
        return await mediator.Send(query, cancellationToken);
    }

    public async Task<Result<DeliveryMetricsDto>> GetDeliveryMetricsAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
    {
        var query = new GetDeliveryMetricsQuery(startDate, endDate);
        return await mediator.Send(query, cancellationToken);
    }

    public async Task<Result<FinancialSummaryDto>> GetFinancialSummaryAsync(CancellationToken cancellationToken = default)
    {
        var query = new GetFinancialSummaryQuery();
        return await mediator.Send(query, cancellationToken);
    }
}
