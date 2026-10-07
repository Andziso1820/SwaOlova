using Microsoft.Extensions.DependencyInjection;
using SwaOlova.Application.Features.Reporting.Services;

namespace SwaOlova.Application.Features.Reporting;

public static class DependencyInjection
{
    public static IServiceCollection AddReportingFeature(this IServiceCollection services)
    {
        services.AddScoped<IReportingOrchestrator, ReportingOrchestrator>();
        return services;
    }
}
