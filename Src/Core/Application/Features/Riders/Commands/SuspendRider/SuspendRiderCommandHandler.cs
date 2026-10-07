using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Riders.Dtos;
using SwaOlova.Domain.Enums;

namespace SwaOlova.Application.Features.Riders.Commands.SuspendRider;

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

        if (rider.Status == RiderStatus.Suspended)
        {
            return Result<SuspendRiderResponse>.Failure("Rider is already suspended.");
        }

        rider.Status = RiderStatus.Suspended;
        await riderRepository.UpdateAsync(rider, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var riderDto = RiderDtoMapper.ToDto(rider);
        return Result<SuspendRiderResponse>.Success(new SuspendRiderResponse(riderDto));
    }
}
