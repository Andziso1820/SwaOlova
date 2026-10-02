using SwaOlova.Application.Common.Abstractions;
using SwaOlova.Application.Features.Dashboard.Dtos;

namespace SwaOlova.Application.Features.Dashboard.Queries.GetRiderDashboard;

public sealed record GetRiderDashboardQuery(Guid RiderId)
    : QueryBase<RiderDashboardDto>;
