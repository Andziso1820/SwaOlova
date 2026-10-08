using Microsoft.Extensions.DependencyInjection;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Infrastructure.Data.Context;
using SwaOlova.Infrastructure.Data.Repositories;
using SwaOlova.Infrastructure.Data.Repositories.Customers;
using SwaOlova.Infrastructure.Data.Repositories.Merchants;
using SwaOlova.Infrastructure.Data.Repositories.Orders;
using SwaOlova.Infrastructure.Data.Repositories.Products;
using SwaOlova.Infrastructure.Data.Repositories.Riders;
using SwaOlova.Infrastructure.Data.Repositories.Deliveries;
using SwaOlova.Infrastructure.Data.Seed;
using SwaOlova.Infrastructure.Data.Persistence;

namespace SwaOlova.Infrastructure.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDataLayer(this IServiceCollection services)
        {
            // Add DbContext
            services.AddDbContext<SwaOlavaDbContext>();

            // Add Seeders
            services.AddScoped<RoleSeeder>();
            services.AddScoped<AdminSeeder>();
            services.AddScoped<VillageSeeder>();
            services.AddScoped<PricingSeeder>();
            services.AddScoped<MerchantCategorySeeder>();
            services.AddScoped<DatabaseInitializer>();

            // Add repositories
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IAuditRepository, AuditRepository>();
            services.AddScoped<ICouponRepository, CouponRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IDeliveryRepository, DeliveryRepository>();
            services.AddScoped<IInventoryRepository, InventoryRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IMerchantRepository, MerchantRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IPromotionRepository, PromotionRepository>();
            services.AddScoped<IRiderRepository, RiderRepository>();

            return services;
        }
    }
}
