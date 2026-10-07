using SwaOlova.Application.Common.Abstractions;
using SwaOlova.Application.Features.Reporting.Dtos;

namespace SwaOlova.Application.Features.Reporting.Queries.GetRevenueReport;

public sealed record GetRevenueReportQuery(DateTime StartDate, DateTime EndDate)
    : QueryBase<GetRevenueReportResponse>;

public sealed record GetRevenueReportResponse(
    IReadOnlyCollection<RevenueReportDto> RevenueData);
