using MediatR;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Locations.Dtos;

namespace SwaOlova.Application.Features.Locations.Commands.CreateZone;

public sealed class CreateZoneCommandHandler
    : IRequestHandler<CreateZoneCommand, Result<CreateZoneResponse>>
{
    public async Task<Result<CreateZoneResponse>> Handle(CreateZoneCommand request, CancellationToken cancellationToken)
    {
        // Create zone with empty villages collection initially
        var zoneDto = new ZoneDto(
            Guid.NewGuid(),
            request.Request.Name.Trim(),
            request.Request.Code.Trim(),
            Array.Empty<VillageDto>(),
            true);

        var response = new CreateZoneResponse(zoneDto);
        return await Task.FromResult(Result<CreateZoneResponse>.Success(response));
    }
}
