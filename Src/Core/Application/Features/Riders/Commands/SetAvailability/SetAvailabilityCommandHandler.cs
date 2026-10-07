using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Riders.Dtos;
using SwaOlova.Domain.Enums;

namespace SwaOlova.Application.Features.Riders.Commands.SetAvailability;

public sealed class SetAvailabilityCommandHandler(
    IRiderRepository riderRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<SetAvailabilityCommand, Result<SetAvailabilityResponse>>
{
    public async Task<Result<SetAvailabilityResponse>> Handle(SetAvailabilityCommand request, CancellationToken cancellationToken)
    {
        var rider = await riderRepository.GetByIdAsync(request.RiderId, cancellationToken);
        if (rider is null)
        {
            return Result<SetAvailabilityResponse>.Failure($"Rider with ID '{request.RiderId}' was not found.");
        }

        if (rider.Status == RiderStatus.Suspended)
        {
            return Result<SetAvailabilityResponse>.Failure("Suspended riders cannot change availability.");
        }

        var newStatus = request.Request.IsAvailable ? RiderStatus.Available : RiderStatus.Offline;
        if (rider.Status == newStatus)
        {
            return Result<SetAvailabilityResponse>.Failure($"Rider is already {newStatus.ToString().ToLower()}.");
        }

        rider.Status = newStatus;
        await riderRepository.UpdateAsync(rider, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var riderDto = RiderDtoMapper.ToDto(rider);
        return Result<SetAvailabilityResponse>.Success(new SetAvailabilityResponse(riderDto));
    }
}
