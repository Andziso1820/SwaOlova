using Microsoft.EntityFrameworkCore;
using SwaOlova.Domain.Merchant;
using SwaOlova.Infrastructure.Data.Context;

namespace SwaOlova.Infrastructure.Data.Seed;

public sealed class MerchantCategorySeeder
{
    private readonly SwaOlavaDbContext _dbContext;

    public MerchantCategorySeeder(SwaOlavaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;
        var categories = new[]
        {
            new MerchantCategory
            {
                Id = Guid.Parse("C2142F68-93DF-47E2-9686-769B75511104"),
                Name = "Restaurant",
                CreatedDate = now,
                CreatedBy = "Seeder"
            },
            new MerchantCategory
            {
                Id = Guid.Parse("16C452E6-C443-407B-A31D-D1C6A54269C6"),
                Name = "Pharmacy",
                CreatedDate = now,
                CreatedBy = "Seeder"
            },
            new MerchantCategory
            {
                Id = Guid.Parse("60EEB5F9-A21E-4DEA-8A2E-1848290D4011"),
                Name = "Grocery Store",
                CreatedDate = now,
                CreatedBy = "Seeder"
            },
            new MerchantCategory
            {
                Id = Guid.Parse("F16E4CB5-36AF-4070-904F-59E5D6AAB611"),
                Name = "Butchery",
                CreatedDate = now,
                CreatedBy = "Seeder"
            },
            new MerchantCategory
            {
                Id = Guid.Parse("9496C2D3-B703-4861-99C0-85D6AEA2BDC8"),
                Name = "Fruit & Vegetable Shop",
                CreatedDate = now,
                CreatedBy = "Seeder"
            },
            new MerchantCategory
            {
                Id = Guid.Parse("857A04C7-467F-4AAF-8BF1-CC4D75BCDD4C"),
                Name = "Factory Shop",
                CreatedDate = now,
                CreatedBy = "Seeder"
            }
        };

        var existingIds = await _dbContext.MerchantCategories
            .Where(x => categories.Select(category => category.Id).Contains(x.Id))
            .Select(x => x.Id)
            .ToListAsync(cancellationToken);

        var missingCategories = categories
            .Where(category => !existingIds.Contains(category.Id))
            .ToArray();

        if (missingCategories.Length == 0)
        {
            return;
        }

        await _dbContext.MerchantCategories.AddRangeAsync(missingCategories, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
