using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Riders.Dtos;
using SwaOlova.Domain.Enums;

namespace SwaOlova.Application.Features.Riders.Commands.ApproveRider;

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

        if (rider.Status != RiderStatus.PendingApproval)
        {
            return Result<ApproveRiderResponse>.Failure("Only riders pending approval can be approved.");
        }

        rider.Status = RiderStatus.Available;
        await riderRepository.UpdateAsync(rider, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var riderDto = RiderDtoMapper.ToDto(rider);
        return Result<ApproveRiderResponse>.Success(new ApproveRiderResponse(riderDto));
    }
}
