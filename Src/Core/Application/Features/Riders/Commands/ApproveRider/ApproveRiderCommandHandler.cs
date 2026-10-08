using MediatR;
using SwaOlova.Application.Common.Abstractions;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Riders.Common;
using SwaOlova.Application.Features.Riders.Dtos;
using SwaOlova.Domain.Enums;
using SwaOlova.Domain.Rider;

namespace SwaOlova.Application.Features.Riders.Commands.ApproveRider;

public sealed record ApproveRiderCommand(Guid RiderId, string? Notes = null)
    : CommandBase<ApproveRiderResponse>;

public sealed class ApproveRiderCommandHandler(
    IRiderRepository riderRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<ApproveRiderCommand, Result<ApproveRiderResponse>>
{
    public async Task<Result<ApproveRiderResponse>> Handle(ApproveRiderCommand request, CancellationToken cancellationToken)
    {
        var rider = await riderRepository.GetByIdAsync(request.RiderId, cancellationToken);
        if (rider is null)
        {
            return Result<ApproveRiderResponse>.Failure($"Rider with ID '{request.RiderId}' was not found.");
        }

        if (!Rider.CanApprove(rider.Status))
        {
            return Result<ApproveRiderResponse>.Failure("Only riders pending approval can be approved.");
        }

        var vehicle = await riderRepository.GetVehicleAsync(rider.Id, cancellationToken);
        if (vehicle is null)
        {
            return Result<ApproveRiderResponse>.Failure("Capture the rider's vehicle before approving.");
        }

        await RiderActivityRecorder.ChangeStatusAsync(
            riderRepository, rider, RiderStatus.Offline, "Rider approved", request.Notes, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<ApproveRiderResponse>.Success(new ApproveRiderResponse(RiderDtoMapper.ToDto(rider, vehicle)));
    }
}
