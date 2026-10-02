using MediatR;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Locations.Dtos;

namespace SwaOlova.Application.Features.Locations.Commands.UpdateVillage;

public sealed class UpdateVillageCommandHandler
    : IRequestHandler<UpdateVillageCommand, Result<UpdateVillageResponse>>
{
    public async Task<Result<UpdateVillageResponse>> Handle(UpdateVillageCommand request, CancellationToken cancellationToken)
    {
        // In a real scenario, this would retrieve and update from a repository
        // For now, creating a response with updated data
        var villageDto = new VillageDto(
            request.VillageId,
            request.Request.Name.Trim(),
            string.Empty, // Code is not updated
            Guid.Empty,   // ZoneId would be fetched from repository
            request.Request.Latitude,
            request.Request.Longitude,
            request.Request.IsActive);

        var response = new UpdateVillageResponse(villageDto);
        return await Task.FromResult(Result<UpdateVillageResponse>.Success(response));
    }
}
