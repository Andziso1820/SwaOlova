using SwaOlova.Application.Common.Abstractions;
using SwaOlova.Application.Features.Reporting.Dtos;

namespace SwaOlova.Application.Features.Reporting.Queries.GetDeliveryMetrics;

public sealed record GetDeliveryMetricsQuery(DateTime StartDate, DateTime EndDate)
    : QueryBase<DeliveryMetricsDto>;
