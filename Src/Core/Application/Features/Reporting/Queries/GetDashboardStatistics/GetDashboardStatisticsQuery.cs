using SwaOlova.Application.Common.Abstractions;
using SwaOlova.Application.Features.Reporting.Dtos;

namespace SwaOlova.Application.Features.Reporting.Queries.GetDashboardStatistics;

public sealed record GetDashboardStatisticsQuery
    : QueryBase<DashboardStatisticsDto>;
