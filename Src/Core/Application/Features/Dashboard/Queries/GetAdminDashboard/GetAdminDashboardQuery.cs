using SwaOlova.Application.Common.Abstractions;
using SwaOlova.Application.Features.Dashboard.Dtos;

namespace SwaOlova.Application.Features.Dashboard.Queries.GetAdminDashboard;

public sealed record GetAdminDashboardQuery
    : QueryBase<AdminDashboardDto>;
