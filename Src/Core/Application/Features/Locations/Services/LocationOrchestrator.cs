using MediatR;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Locations.Commands.ConfigureCoverage;
using SwaOlova.Application.Features.Locations.Commands.CreateVillage;
using SwaOlova.Application.Features.Locations.Commands.CreateZone;
using SwaOlova.Application.Features.Locations.Commands.UpdateVillage;
using SwaOlova.Application.Features.Locations.Dtos;
using SwaOlova.Application.Features.Locations.Queries.GetCoverageByArea;
using SwaOlova.Application.Features.Locations.Queries.GetVillageById;
using SwaOlova.Application.Features.Locations.Queries.GetZoneById;

namespace SwaOlova.Application.Features.Locations.Services;

public sealed class LocationOrchestrator(IMediator mediator)
    : ILocationOrchestrator
{
    public async Task<Result<VillageDto>> CreateVillageAsync(CreateVillageRequest request, CancellationToken cancellationToken = default)
    {
        var command = new CreateVillageCommand(request);
        var result = await mediator.Send(command, cancellationToken);
        return result.IsSuccess ? Result<VillageDto>.Success(result.Value.Village) : Result<VillageDto>.Failure(result.Error);
    }

    public async Task<Result<VillageDto>> UpdateVillageAsync(Guid villageId, UpdateVillageRequest request, CancellationToken cancellationToken = default)
    {
        var command = new UpdateVillageCommand(villageId, request);
        var result = await mediator.Send(command, cancellationToken);
        return result.IsSuccess ? Result<VillageDto>.Success(result.Value.Village) : Result<VillageDto>.Failure(result.Error);
    }

    public async Task<Result<ZoneDto>> CreateZoneAsync(CreateZoneRequest request, CancellationToken cancellationToken = default)
    {
        var command = new CreateZoneCommand(request);
        var result = await mediator.Send(command, cancellationToken);
        return result.IsSuccess ? Result<ZoneDto>.Success(result.Value.Zone) : Result<ZoneDto>.Failure(result.Error);
    }

    public async Task<Result> ConfigureCoverageAsync(ConfigureCoverageRequest request, CancellationToken cancellationToken = default)
    {
        var command = new ConfigureCoverageCommand(request);
        return await mediator.Send(command, cancellationToken);
    }

    public async Task<Result<VillageDto>> GetVillageByIdAsync(Guid villageId, CancellationToken cancellationToken = default)
    {
        var query = new GetVillageByIdQuery(villageId);
        return await mediator.Send(query, cancellationToken);
    }

    public async Task<Result<ZoneDto>> GetZoneByIdAsync(Guid zoneId, CancellationToken cancellationToken = default)
    {
        var query = new GetZoneByIdQuery(zoneId);
        return await mediator.Send(query, cancellationToken);
    }

    public async Task<Result<GetCoverageByAreaResponse>> GetCoverageByAreaAsync(Guid merchantId, Guid zoneId, CancellationToken cancellationToken = default)
    {
        var query = new GetCoverageByAreaQuery(merchantId, zoneId);
        return await mediator.Send(query, cancellationToken);
    }
}
