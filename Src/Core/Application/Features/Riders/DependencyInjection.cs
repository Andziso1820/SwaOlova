using Microsoft.Extensions.DependencyInjection;
using SwaOlova.Application.Features.Riders.Services;

namespace SwaOlova.Application.Features.Riders;

public static class DependencyInjection
{
    public static IServiceCollection AddRidersFeature(this IServiceCollection services)
    {
        services.AddScoped<IRiderOrchestrator, RiderOrchestrator>();
        return services;
    }
}
