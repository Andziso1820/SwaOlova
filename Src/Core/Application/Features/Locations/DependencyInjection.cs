using Microsoft.Extensions.DependencyInjection;
using SwaOlova.Application.Features.Locations.Services;

namespace SwaOlova.Application.Features.Locations;

public static class DependencyInjection
{
    public static IServiceCollection AddLocationsFeature(this IServiceCollection services)
    {
        services.AddScoped<ILocationOrchestrator, LocationOrchestrator>();
        return services;
    }
}
