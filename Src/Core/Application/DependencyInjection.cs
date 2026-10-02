using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SwaOlova.Application.Common.Behaviors;
using SwaOlova.Application.Features.Customers;
using SwaOlova.Application.Features.Dashboard;
using SwaOlova.Application.Features.Identity;
using SwaOlova.Application.Features.Locations;
using SwaOlova.Application.Features.Merchants;
using SwaOlova.Application.Features.Notifications;
using SwaOlova.Application.Features.Orders;
using SwaOlova.Application.Features.Payments;
using SwaOlova.Application.Features.Products;
using SwaOlova.Application.Features.Promotions;
using SwaOlova.Application.Features.Reporting;
using SwaOlova.Application.Features.Riders;

namespace SwaOlova.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(DependencyInjection).Assembly);
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionHandlingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuditBehavior<,>));

        services
            .AddCustomersFeature()
            .AddMerchantsFeature()
            .AddProductsFeature()
            .AddOrdersFeature()
            .AddLocationsFeature()
            .AddPaymentsFeature()
            .AddPromotionsFeature()
            .AddNotificationsFeature()
            .AddReportingFeature()
            .AddIdentityFeature()
            .AddDashboardFeature()
            .AddRidersFeature();

        return services;
    }
}
