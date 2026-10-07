using SwaOlova.Application.Common.Abstractions;
using SwaOlova.Application.Features.Reporting.Dtos;

namespace SwaOlova.Application.Features.Reporting.Queries.GetMerchantPerformance;

public sealed record GetMerchantPerformanceQuery
    : QueryBase<GetMerchantPerformanceResponse>;

public sealed record GetMerchantPerformanceResponse(
    IReadOnlyCollection<PerformanceMetricDto> Metrics);
