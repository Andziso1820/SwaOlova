using Microsoft.EntityFrameworkCore;
using SwaOlova.Infrastructure.Data.Context;
using SwaOlova.Infrastructure.Data.Seed;

namespace SwaOlova.Infrastructure.Data.Persistence;

public sealed class DatabaseInitializer
{
    private readonly SwaOlavaDbContext _dbContext;
    private readonly RoleSeeder _roleSeeder;
    private readonly AdminSeeder _adminSeeder;
    private readonly VillageSeeder _villageSeeder;
    private readonly PricingSeeder _pricingSeeder;

    public DatabaseInitializer(
        SwaOlavaDbContext dbContext,
        RoleSeeder roleSeeder,
        AdminSeeder adminSeeder,
        VillageSeeder villageSeeder,
        PricingSeeder pricingSeeder)
    {
        _dbContext = dbContext;
        _roleSeeder = roleSeeder;
        _adminSeeder = adminSeeder;
        _villageSeeder = villageSeeder;
        _pricingSeeder = pricingSeeder;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.Database.EnsureCreatedAsync(cancellationToken);
        //await _dbContext.Database.MigrateAsync(cancellationToken);
        await _roleSeeder.SeedAsync(cancellationToken);
        await _villageSeeder.SeedAsync(cancellationToken);
        await _pricingSeeder.SeedAsync(cancellationToken);
        await _adminSeeder.SeedAsync(cancellationToken);
    }
}
