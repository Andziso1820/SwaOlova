using FluentValidation;
using MediatR;
using SwaOlova.Application.Common.Abstractions;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Riders.Common;
using SwaOlova.Application.Features.Riders.Dtos;
using SwaOlova.Domain.Enums;
using SwaOlova.Domain.Rider;

namespace SwaOlova.Application.Features.Riders.Commands.SuspendRider;

public sealed record SuspendRiderCommand(Guid RiderId, string Reason)
    : CommandBase<SuspendRiderResponse>;

public sealed class SuspendRiderCommandValidator : AbstractValidator<SuspendRiderCommand>
{
    public SuspendRiderCommandValidator()
    {
        RuleFor(x => x.RiderId).NotEmpty();
        RuleFor(x => x.Reason).NotEmpty().MaximumLength(1000);
    }
}

public sealed class SuspendRiderCommandHandler(
    IRiderRepository riderRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<SuspendRiderCommand, Result<SuspendRiderResponse>>
{
    public async Task<Result<SuspendRiderResponse>> Handle(SuspendRiderCommand request, CancellationToken cancellationToken)
    {
        var rider = await riderRepository.GetByIdAsync(request.RiderId, cancellationToken);
        if (rider is null)
        {
            return Result<SuspendRiderResponse>.Failure($"Rider with ID '{request.RiderId}' was not found.");
        }

        if (!Rider.CanSuspend(rider.Status))
        {
            return Result<SuspendRiderResponse>.Failure($"A rider with status '{rider.Status}' cannot be suspended.");
        }

        await RiderActivityRecorder.ChangeStatusAsync(
            riderRepository, rider, RiderStatus.Suspended, "Rider suspended", request.Reason, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<SuspendRiderResponse>.Success(new SuspendRiderResponse(RiderDtoMapper.ToDto(rider)));
    }
}
