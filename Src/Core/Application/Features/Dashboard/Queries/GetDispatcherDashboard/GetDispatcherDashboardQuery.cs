using SwaOlova.Application.Common.Abstractions;
using SwaOlova.Application.Features.Dashboard.Dtos;

namespace SwaOlova.Application.Features.Dashboard.Queries.GetDispatcherDashboard;

public sealed record GetDispatcherDashboardQuery
    : QueryBase<DispatcherDashboardDto>;
