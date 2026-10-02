using Microsoft.Extensions.DependencyInjection;
using SwaOlova.Application.Features.Identity.Services;

namespace SwaOlova.Application.Features.Identity;

public static class DependencyInjection
{
    public static IServiceCollection AddIdentityFeature(this IServiceCollection services)
    {
        services.AddScoped<IIdentityOrchestrator, IdentityOrchestrator>();
        return services;
    }
}
