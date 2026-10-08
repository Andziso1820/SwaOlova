using FluentValidation;
using MediatR;
using SwaOlova.Application.Common.Abstractions;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Riders.Common;
using SwaOlova.Application.Features.Riders.Dtos;
using SwaOlova.Domain.Enums;
using SwaOlova.Domain.Rider;

namespace SwaOlova.Application.Features.Riders.Commands.SetAvailability;

public sealed record SetAvailabilityCommand(Guid RiderId, SetAvailabilityRequest Request)
    : CommandBase<SetAvailabilityResponse>;

public sealed class SetAvailabilityCommandValidator : AbstractValidator<SetAvailabilityCommand>
{
    public SetAvailabilityCommandValidator()
    {
        RuleFor(x => x.RiderId).NotEmpty();
        RuleFor(x => x.Request).NotNull();
    }
}

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

        if (!Rider.CanChangeAvailability(rider.Status))
        {
            return Result<SetAvailabilityResponse>.Failure(rider.Status switch
            {
                RiderStatus.PendingApproval => "The rider must be approved before availability can be changed.",
                RiderStatus.Suspended => "Suspended riders cannot change availability.",
                RiderStatus.Busy => "Riders on an active delivery cannot change availability.",
                _ => $"Availability cannot be changed while the rider is '{rider.Status}'."
            });
        }

        var newStatus = request.Request.IsAvailable ? RiderStatus.Available : RiderStatus.Offline;
        if (rider.Status == newStatus)
        {
            return Result<SetAvailabilityResponse>.Failure($"Rider is already {newStatus.ToString().ToLowerInvariant()}.");
        }

        await RiderActivityRecorder.ChangeStatusAsync(
            riderRepository,
            rider,
            newStatus,
            request.Request.IsAvailable ? "Rider went online" : "Rider went offline",
            null,
            cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<SetAvailabilityResponse>.Success(new SetAvailabilityResponse(RiderDtoMapper.ToDto(rider)));
    }
}
