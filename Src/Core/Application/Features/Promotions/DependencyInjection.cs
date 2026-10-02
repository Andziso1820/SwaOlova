using Microsoft.Extensions.DependencyInjection;
using SwaOlova.Application.Features.Promotions.Services;

namespace SwaOlova.Application.Features.Promotions;

public static class DependencyInjection
{
    public static IServiceCollection AddPromotionsFeature(this IServiceCollection services)
    {
        services.AddScoped<IPromotionOrchestrator, PromotionOrchestrator>();
        return services;
    }
}
