using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Locations.Commands.ConfigureCoverage;
using SwaOlova.Application.Features.Locations.Commands.CreateVillage;
using SwaOlova.Application.Features.Locations.Commands.CreateZone;
using SwaOlova.Application.Features.Locations.Commands.UpdateVillage;
using SwaOlova.Application.Features.Locations.Dtos;
using SwaOlova.Application.Features.Locations.Queries.GetCoverageByArea;

namespace SwaOlova.Application.Features.Locations.Services;

public interface ILocationOrchestrator
{
    Task<Result<VillageDto>> CreateVillageAsync(CreateVillageRequest request, CancellationToken cancellationToken = default);
    Task<Result<VillageDto>> UpdateVillageAsync(Guid villageId, UpdateVillageRequest request, CancellationToken cancellationToken = default);
    Task<Result<ZoneDto>> CreateZoneAsync(CreateZoneRequest request, CancellationToken cancellationToken = default);
    Task<Result> ConfigureCoverageAsync(ConfigureCoverageRequest request, CancellationToken cancellationToken = default);
    Task<Result<VillageDto>> GetVillageByIdAsync(Guid villageId, CancellationToken cancellationToken = default);
    Task<Result<ZoneDto>> GetZoneByIdAsync(Guid zoneId, CancellationToken cancellationToken = default);
    Task<Result<GetCoverageByAreaResponse>> GetCoverageByAreaAsync(Guid merchantId, Guid zoneId, CancellationToken cancellationToken = default);
}
