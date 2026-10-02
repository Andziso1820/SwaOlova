using Microsoft.Extensions.DependencyInjection;
using SwaOlova.Application.Features.Products.Services;

namespace SwaOlova.Application.Features.Products;

public static class DependencyInjection
{
    public static IServiceCollection AddProductsFeature(this IServiceCollection services)
    {
        services.AddScoped<IProductOrchestrator, ProductOrchestrator>();
        return services;
    }
}
