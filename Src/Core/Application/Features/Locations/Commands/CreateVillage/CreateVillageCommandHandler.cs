using MediatR;
using SwaOlova.Application.Common.Models;

namespace SwaOlova.Application.Features.Locations.Commands.CreateVillage;

public sealed class CreateVillageCommandHandler
    : IRequestHandler<CreateVillageCommand, Result<CreateVillageResponse>>
{
    public async Task<Result<CreateVillageResponse>> Handle(CreateVillageCommand request, CancellationToken cancellationToken)
    {
        // In a real scenario, this would persist to a repository
        // For now, we're creating a DTO-based response
        var villageDto = new CreateVillageResponse(
            new SwaOlova.Application.Features.Locations.Dtos.VillageDto(
                Guid.NewGuid(),
                request.Request.Name.Trim(),
                request.Request.Code.Trim(),
                request.Request.ZoneId,
                request.Request.Latitude,
                request.Request.Longitude,
                true));

        return await Task.FromResult(Result<CreateVillageResponse>.Success(villageDto));
    }
}
