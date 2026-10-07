using Microsoft.Extensions.DependencyInjection;
using SwaOlova.Application.Features.Merchants.Services;

namespace SwaOlova.Application.Features.Merchants;

public static class DependencyInjection
{
    public static IServiceCollection AddMerchantsFeature(this IServiceCollection services)
    {
        services.AddScoped<IMerchantOrchestrator, MerchantOrchestrator>();
        return services;
    }
}
