using Microsoft.Extensions.DependencyInjection;
using SwaOlova.Application.Features.Orders.Services;

namespace SwaOlova.Application.Features.Orders;

public static class DependencyInjection
{
    public static IServiceCollection AddOrdersFeature(this IServiceCollection services)
    {
        services.AddScoped<IOrderOrchestrator, OrderOrchestrator>();
        return services;
    }
}
