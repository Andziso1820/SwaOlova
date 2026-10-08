using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Riders.Common;
using SwaOlova.Application.Features.Riders.Dtos;
using SwaOlova.Domain.Enums;
using SwaOlova.Domain.Rider;

namespace SwaOlova.Application.Features.Riders.Commands.UpdateLocation;

public sealed class UpdateLocationCommandHandler(
    IRiderRepository riderRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateLocationCommand, Result<UpdateLocationResponse>>
{
    public async Task<Result<UpdateLocationResponse>> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
    {
        var rider = await riderRepository.GetByIdAsync(request.RiderId, cancellationToken);
        if (rider is null)
        {
            return Result<UpdateLocationResponse>.Failure($"Rider with ID '{request.RiderId}' was not found.");
        }

        if (!Rider.CanTrackLocation(rider.Status))
        {
            return Result<UpdateLocationResponse>.Failure($"Location cannot be recorded while the rider is '{rider.Status}'.");
        }

        var location = new RiderLocation
        {
            Id = Guid.NewGuid(),
            RiderId = rider.Id,
            Latitude = request.Request.Latitude,
            Longitude = request.Request.Longitude,
            RecordedAt = DateTime.UtcNow
        };

        await riderRepository.AddLocationAsync(location, cancellationToken);

        await RiderActivityRecorder.RecordAsync(
            riderRepository,
            rider.Id,
            RiderActivityType.LocationUpdated,
            "Location updated",
            $"Location recorded at {location.Latitude:F6}, {location.Longitude:F6}.",
            cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<UpdateLocationResponse>.Success(new UpdateLocationResponse(RiderDtoMapper.ToLocationDto(location)));
    }
}
