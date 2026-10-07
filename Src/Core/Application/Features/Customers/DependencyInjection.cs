using Microsoft.Extensions.DependencyInjection;

namespace SwaOlova.Application.Features.Customers;

public static class DependencyInjection
{
    public static IServiceCollection AddCustomersFeature(this IServiceCollection services)
    {
        return services;
    }
}
