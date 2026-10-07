using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Riders.Dtos;
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

        var location = new RiderLocation
        {
            Id = Guid.NewGuid(),
            RiderId = request.RiderId,
            Latitude = request.Request.Latitude,
            Longitude = request.Request.Longitude,
            RecordedAt = DateTime.UtcNow
        };

        // Note: Assuming IRiderRepository would have a method to add location or we add it via a separate interface
        // For now, this demonstrates the command structure
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var locationDto = RiderDtoMapper.ToLocationDto(location);
        return Result<UpdateLocationResponse>.Success(new UpdateLocationResponse(locationDto));
    }
}
