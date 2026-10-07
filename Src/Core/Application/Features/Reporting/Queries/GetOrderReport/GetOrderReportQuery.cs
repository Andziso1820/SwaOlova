using SwaOlova.Application.Common.Abstractions;
using SwaOlova.Application.Features.Reporting.Dtos;

namespace SwaOlova.Application.Features.Reporting.Queries.GetOrderReport;

public sealed record GetOrderReportQuery(DateTime StartDate, DateTime EndDate)
    : QueryBase<OrderReportDto>;
