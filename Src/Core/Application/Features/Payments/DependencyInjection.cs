using Microsoft.Extensions.DependencyInjection;
using SwaOlova.Application.Features.Payments.Services;

namespace SwaOlova.Application.Features.Payments;

public static class DependencyInjection
{
    public static IServiceCollection AddPaymentsFeature(this IServiceCollection services)
    {
        services.AddScoped<IPaymentOrchestrator, PaymentOrchestrator>();
        return services;
    }
}
