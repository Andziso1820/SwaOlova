using MediatR;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Locations.Dtos;

namespace SwaOlova.Application.Features.Locations.Queries.GetVillageById;

public sealed class GetVillageByIdQueryHandler
    : IRequestHandler<GetVillageByIdQuery, Result<VillageDto>>
{
    public async Task<Result<VillageDto>> Handle(GetVillageByIdQuery request, CancellationToken cancellationToken)
    {
        // In a real scenario, this would query from a repository
        // For now, returning a placeholder
        var villageDto = new VillageDto(
            request.VillageId,
            "Sample Village",
            "SAMPLE_VILLAGE",
            Guid.NewGuid(),
            0.0m,
            0.0m,
            true);

        return await Task.FromResult(Result<VillageDto>.Success(villageDto));
    }
}
