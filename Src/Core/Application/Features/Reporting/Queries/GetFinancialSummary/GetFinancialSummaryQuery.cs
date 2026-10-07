using SwaOlova.Application.Common.Abstractions;
using SwaOlova.Application.Features.Reporting.Dtos;

namespace SwaOlova.Application.Features.Reporting.Queries.GetFinancialSummary;

public sealed record GetFinancialSummaryQuery
    : QueryBase<FinancialSummaryDto>;
