using SwaOlova.Domain.Auditing;
using SwaOlova.Domain.Delivery;
using SwaOlova.Domain.Notifications;
using SwaOlova.Domain.Payment;
using SwaOlova.Domain.Promotions;
using SwaOlova.Domain.Product;
using SwaOlova.Domain.Rider;
using SwaOlova.Infrastructure.Data.Context;

namespace SwaOlova.Infrastructure.Data.Repositories;



public sealed class PaymentRepository : Repository<Payment>, SwaOlova.Application.Common.Interfaces.Repositories.IPaymentRepository
{
    public PaymentRepository(SwaOlavaDbContext dbContext)
        : base(dbContext)
    {
    }
}

public sealed class PromotionRepository : Repository<Promotion>, SwaOlova.Application.Common.Interfaces.Repositories.IPromotionRepository
{
    public PromotionRepository(SwaOlavaDbContext dbContext)
        : base(dbContext)
    {
    }
}

public sealed class CouponRepository : Repository<Coupon>, SwaOlova.Application.Common.Interfaces.Repositories.ICouponRepository
{
    public CouponRepository(SwaOlavaDbContext dbContext)
        : base(dbContext)
    {
    }
}

public sealed class InventoryRepository : Repository<Inventory>, SwaOlova.Application.Common.Interfaces.Repositories.IInventoryRepository
{
    public InventoryRepository(SwaOlavaDbContext dbContext)
        : base(dbContext)
    {
    }
}

public sealed class LocationRepository : Repository<RiderLocation>, SwaOlova.Application.Common.Interfaces.Repositories.ILocationRepository
{
    public LocationRepository(SwaOlavaDbContext dbContext)
        : base(dbContext)
    {
    }
}

public sealed class NotificationRepository : Repository<Notification>, SwaOlova.Application.Common.Interfaces.Repositories.INotificationRepository
{
    public NotificationRepository(SwaOlavaDbContext dbContext)
        : base(dbContext)
    {
    }
}

public sealed class AuditRepository : Repository<AuditLog>, SwaOlova.Application.Common.Interfaces.Repositories.IAuditRepository
{
    public AuditRepository(SwaOlavaDbContext dbContext)
        : base(dbContext)
    {
    }
}
