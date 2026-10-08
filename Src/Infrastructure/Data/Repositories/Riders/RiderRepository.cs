using Microsoft.EntityFrameworkCore;
using SwaOlova.Application.Common.Interfaces.Services;
using SwaOlova.Domain.Enums;
using SwaOlova.Domain.Rider;
using SwaOlova.Domain.Vehicle;
using SwaOlova.Infrastructure.Data.Context;

namespace SwaOlova.Infrastructure.Data.Repositories.Riders;

public sealed class RiderRepository : Repository<Rider>, SwaOlova.Application.Common.Interfaces.Repositories.IRiderRepository
{
    private readonly ICurrentUserService _currentUserService;

    public RiderRepository(SwaOlavaDbContext dbContext, ICurrentUserService currentUserService)
        : base(dbContext)
    {
        _currentUserService = currentUserService;
    }

    public override async Task<Rider?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbContext.Riders
            .Include(x => x.Documents.OrderByDescending(d => d.CreatedDate))
            .AsSplitQuery()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public override async Task<IReadOnlyCollection<Rider>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await DbContext.Riders
            .AsNoTracking()
            .OrderBy(x => x.FirstName)
            .ThenBy(x => x.LastName)
            .ToListAsync(cancellationToken);
    }

    public override async Task AddAsync(Rider entity, CancellationToken cancellationToken = default)
    {
        StampCreated(entity);
        await base.AddAsync(entity, cancellationToken);
    }

    public override Task UpdateAsync(Rider entity, CancellationToken cancellationToken = default)
    {
        entity.ModifiedDate = DateTime.UtcNow;
        entity.ModifiedBy = GetCurrentUserName();

        if (DbContext.Entry(entity).State == EntityState.Detached)
        {
            Set.Update(entity);
        }

        return Task.CompletedTask;
    }

    public async Task<(IReadOnlyCollection<Rider> Items, int TotalCount)> SearchAsync(
        string? keyword,
        RiderStatus? status,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var query = ApplyKeyword(DbContext.Riders.AsNoTracking(), keyword);

        if (status.HasValue)
        {
            query = query.Where(x => x.Status == status.Value);
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderBy(x => x.FirstName)
            .ThenBy(x => x.LastName)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (items, totalCount);
    }

    public async Task<IReadOnlyDictionary<RiderStatus, int>> GetStatusCountsAsync(string? keyword, CancellationToken cancellationToken = default)
    {
        return await ApplyKeyword(DbContext.Riders.AsNoTracking(), keyword)
            .GroupBy(x => x.Status)
            .Select(g => new { Status = g.Key, Count = g.Count() })
            .ToDictionaryAsync(x => x.Status, x => x.Count, cancellationToken);
    }

    public async Task<bool> PhoneNumberExistsAsync(string phoneNumber, Guid? excludeRiderId = null, CancellationToken cancellationToken = default)
    {
        var value = phoneNumber.Trim();
        return await DbContext.Riders.AnyAsync(
            x => x.PhoneNumber == value && (!excludeRiderId.HasValue || x.Id != excludeRiderId.Value),
            cancellationToken);
    }

    public async Task<bool> DriversLicenseExistsAsync(string driversLicenseNumber, Guid? excludeRiderId = null, CancellationToken cancellationToken = default)
    {
        var value = driversLicenseNumber.Trim();
        return await DbContext.Riders.AnyAsync(
            x => x.DriversLicenseNumber == value && (!excludeRiderId.HasValue || x.Id != excludeRiderId.Value),
            cancellationToken);
    }

    public async Task<IReadOnlyCollection<RiderActivity>> GetActivityHistoryAsync(Guid riderId, CancellationToken cancellationToken = default)
    {
        return await DbContext.RiderActivities
            .AsNoTracking()
            .Where(x => x.RiderId == riderId)
            .OrderByDescending(x => x.CreatedDate)
            .ToListAsync(cancellationToken);
    }

    public async Task AddActivityAsync(RiderActivity activity, CancellationToken cancellationToken = default)
    {
        StampCreated(activity);
        await DbContext.RiderActivities.AddAsync(activity, cancellationToken);
    }

    public async Task<RiderDocument?> GetDocumentByIdAsync(Guid documentId, CancellationToken cancellationToken = default)
    {
        return await DbContext.RiderDocuments
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == documentId, cancellationToken);
    }

    public async Task AddDocumentAsync(RiderDocument document, CancellationToken cancellationToken = default)
    {
        StampCreated(document);
        await DbContext.RiderDocuments.AddAsync(document, cancellationToken);
    }

    public async Task<RiderLocation?> GetLatestLocationAsync(Guid riderId, CancellationToken cancellationToken = default)
    {
        return await DbContext.RiderLocations
            .AsNoTracking()
            .Where(x => x.RiderId == riderId)
            .OrderByDescending(x => x.RecordedAt)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task AddLocationAsync(RiderLocation location, CancellationToken cancellationToken = default)
    {
        StampCreated(location);
        await DbContext.RiderLocations.AddAsync(location, cancellationToken);
    }

    public async Task<Vehicle?> GetVehicleAsync(Guid riderId, CancellationToken cancellationToken = default)
    {
        return await DbContext.Vehicles
            .FirstOrDefaultAsync(x => x.RiderId == riderId, cancellationToken);
    }

    public async Task<bool> RegistrationNumberExistsAsync(string registrationNumber, Guid? excludeRiderId = null, CancellationToken cancellationToken = default)
    {
        var value = registrationNumber.Trim();
        return await DbContext.Vehicles.AnyAsync(
            x => x.RegistrationNumber == value && (!excludeRiderId.HasValue || x.RiderId != excludeRiderId.Value),
            cancellationToken);
    }

    public async Task AddVehicleAsync(Vehicle vehicle, CancellationToken cancellationToken = default)
    {
        StampCreated(vehicle);
        await DbContext.Vehicles.AddAsync(vehicle, cancellationToken);
    }

    private static IQueryable<Rider> ApplyKeyword(IQueryable<Rider> query, string? keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
        {
            return query;
        }

        var pattern = $"%{keyword.Trim()}%";
        return query.Where(x =>
            EF.Functions.Like(x.RiderNumber, pattern) ||
            EF.Functions.Like(x.FirstName + " " + x.LastName, pattern) ||
            EF.Functions.Like(x.PhoneNumber, pattern) ||
            EF.Functions.Like(x.DriversLicenseNumber, pattern) ||
            (x.Email != null && EF.Functions.Like(x.Email, pattern)));
    }

    private void StampCreated(SwaOlova.Domain.Common.AuditableEntity<Guid> entity)
    {
        if (entity.CreatedDate == default)
        {
            entity.CreatedDate = DateTime.UtcNow;
        }

        if (string.IsNullOrWhiteSpace(entity.CreatedBy))
        {
            entity.CreatedBy = GetCurrentUserName();
        }
    }

    private string GetCurrentUserName()
    {
        return _currentUserService.UserName
            ?? _currentUserService.UserId
            ?? "system";
    }
}
