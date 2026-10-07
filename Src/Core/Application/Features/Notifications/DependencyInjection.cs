using Microsoft.Extensions.DependencyInjection;
using SwaOlova.Application.Features.Notifications.Services;

namespace SwaOlova.Application.Features.Notifications;

public static class DependencyInjection
{
    public static IServiceCollection AddNotificationsFeature(this IServiceCollection services)
    {
        services.AddScoped<INotificationOrchestrator, NotificationOrchestrator>();
        return services;
    }
}
