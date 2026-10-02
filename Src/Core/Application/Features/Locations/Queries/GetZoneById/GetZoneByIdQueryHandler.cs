using MediatR;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Locations.Dtos;

namespace SwaOlova.Application.Features.Locations.Queries.GetZoneById;

public sealed class GetZoneByIdQueryHandler
    : IRequestHandler<GetZoneByIdQuery, Result<ZoneDto>>
{
    public async Task<Result<ZoneDto>> Handle(GetZoneByIdQuery request, CancellationToken cancellationToken)
    {
        // In a real scenario, this would query from a repository with loaded villages
        // For now, returning a placeholder with empty villages
        var zoneDto = new ZoneDto(
            request.ZoneId,
            "Sample Zone",
            "SAMPLE_ZONE",
            Array.Empty<VillageDto>(),
            true);

        return await Task.FromResult(Result<ZoneDto>.Success(zoneDto));
    }
}
