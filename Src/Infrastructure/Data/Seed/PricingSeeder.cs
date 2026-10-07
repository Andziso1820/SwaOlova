using Microsoft.EntityFrameworkCore;
using SwaOlova.Domain.Promotions;
using SwaOlova.Infrastructure.Data.Context;

namespace SwaOlova.Infrastructure.Data.Seed;

public sealed class PricingSeeder
{
    private readonly SwaOlavaDbContext _dbContext;

    public PricingSeeder(SwaOlavaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        if (await _dbContext.Promotions.AnyAsync(cancellationToken) || await _dbContext.Coupons.AnyAsync(cancellationToken))
        {
            return;
        }

        var now = DateTime.UtcNow;

        var promotion = new Promotion
        {
            Id = Guid.NewGuid(),
            Name = "Welcome Promotion",
            DiscountValue = 10m,
            StartDate = now,
            EndDate = now.AddMonths(1),
            IsActive = true,
            CreatedDate = now,
            CreatedBy = "Seeder"
        };

        var coupon = new Coupon
        {
            Id = Guid.NewGuid(),
            Code = "WELCOME10",
            DiscountAmount = 10m,
            ExpiryDate = now.AddMonths(1),
            CreatedDate = now,
            CreatedBy = "Seeder"
        };

        await _dbContext.Promotions.AddAsync(promotion, cancellationToken);
        await _dbContext.Coupons.AddAsync(coupon, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
