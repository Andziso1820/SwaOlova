using SwaOlova.Application.Common.Abstractions;
using SwaOlova.Application.Features.Reporting.Dtos;

namespace SwaOlova.Application.Features.Reporting.Queries.GetRiderPerformance;

public sealed record GetRiderPerformanceQuery
    : QueryBase<GetRiderPerformanceResponse>;

public sealed record GetRiderPerformanceResponse(
    IReadOnlyCollection<PerformanceMetricDto> Metrics);
