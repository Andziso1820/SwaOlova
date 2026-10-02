using Microsoft.EntityFrameworkCore;
using SwaOlova.Domain.Locations;
using SwaOlova.Infrastructure.Data.Context;

namespace SwaOlova.Infrastructure.Data.Seed;

public sealed class VillageSeeder
{
    private readonly SwaOlavaDbContext _dbContext;

    public VillageSeeder(SwaOlavaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        if (await _dbContext.Zones.AnyAsync(cancellationToken) || await _dbContext.Villages.AnyAsync(cancellationToken))
        {
            return;
        }

        var zone = new Zone
        {
            Id = Guid.NewGuid(),
            Name = "Central Zone",
            Code = "CENTRAL",
            IsActive = true,
            CreatedDate = DateTime.UtcNow,
            CreatedBy = "Seeder"
        };

        var village = new Village
        {
            Id = Guid.NewGuid(),
            Name = "Central Village",
            Code = "CENTRAL_VILLAGE",
            ZoneId = zone.Id,
            Latitude = 0m,
            Longitude = 0m,
            IsActive = true,
            CreatedDate = DateTime.UtcNow,
            CreatedBy = "Seeder"
        };

        await _dbContext.Zones.AddAsync(zone, cancellationToken);
        await _dbContext.Villages.AddAsync(village, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
