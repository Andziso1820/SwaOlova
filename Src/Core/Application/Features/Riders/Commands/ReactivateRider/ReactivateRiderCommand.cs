using FluentValidation;
using MediatR;
using SwaOlova.Application.Common.Abstractions;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Riders.Common;
using SwaOlova.Application.Features.Riders.Dtos;
using SwaOlova.Domain.Enums;
using SwaOlova.Domain.Rider;

namespace SwaOlova.Application.Features.Riders.Commands.ReactivateRider;

public sealed record ReactivateRiderResponse(RiderDto Rider);

public sealed record ReactivateRiderCommand(Guid RiderId, string Reason)
    : CommandBase<ReactivateRiderResponse>;

public sealed class ReactivateRiderCommandValidator : AbstractValidator<ReactivateRiderCommand>
{
    public ReactivateRiderCommandValidator()
    {
        RuleFor(x => x.RiderId).NotEmpty();
        RuleFor(x => x.Reason).NotEmpty().MaximumLength(1000);
    }
}

public sealed class ReactivateRiderCommandHandler(
    IRiderRepository riderRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<ReactivateRiderCommand, Result<ReactivateRiderResponse>>
{
    public async Task<Result<ReactivateRiderResponse>> Handle(ReactivateRiderCommand request, CancellationToken cancellationToken)
    {
        var rider = await riderRepository.GetByIdAsync(request.RiderId, cancellationToken);
        if (rider is null)
        {
            return Result<ReactivateRiderResponse>.Failure($"Rider with ID '{request.RiderId}' was not found.");
        }

        if (!Rider.CanReactivate(rider.Status))
        {
            return Result<ReactivateRiderResponse>.Failure("Only suspended riders can be reactivated.");
        }

        await RiderActivityRecorder.ChangeStatusAsync(
            riderRepository, rider, RiderStatus.Offline, "Rider reactivated", request.Reason, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<ReactivateRiderResponse>.Success(new ReactivateRiderResponse(RiderDtoMapper.ToDto(rider)));
    }
}
