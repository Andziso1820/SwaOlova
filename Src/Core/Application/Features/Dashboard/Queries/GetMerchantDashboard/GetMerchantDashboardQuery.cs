using SwaOlova.Application.Common.Abstractions;
using SwaOlova.Application.Features.Dashboard.Dtos;

namespace SwaOlova.Application.Features.Dashboard.Queries.GetMerchantDashboard;

public sealed record GetMerchantDashboardQuery(Guid MerchantId)
    : QueryBase<MerchantDashboardDto>;
