using Microsoft.Extensions.DependencyInjection;
using SwaOlova.Application.Features.Dashboard.Services;

namespace SwaOlova.Application.Features.Dashboard;

public static class DependencyInjection
{
    public static IServiceCollection AddDashboardFeature(this IServiceCollection services)
    {
        services.AddScoped<IDashboardOrchestrator, DashboardOrchestrator>();
        return services;
    }
}
