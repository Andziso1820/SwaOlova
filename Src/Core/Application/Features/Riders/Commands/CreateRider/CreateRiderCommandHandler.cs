using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Riders.Dtos;
using SwaOlova.Domain.Enums;
using SwaOlova.Domain.Rider;

namespace SwaOlova.Application.Features.Riders.Commands.CreateRider;

public sealed class CreateRiderCommandHandler(
    IRiderRepository riderRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateRiderCommand, Result<CreateRiderResponse>>
{
    public async Task<Result<CreateRiderResponse>> Handle(CreateRiderCommand request, CancellationToken cancellationToken)
    {
        var rider = new Rider
        {
            Id = Guid.NewGuid(),
            RiderNumber = $"RID-{DateTime.UtcNow:yyyyMMddHHmmssfff}",
            FirstName = request.Request.FirstName.Trim(),
            LastName = request.Request.LastName.Trim(),
            PhoneNumber = request.Request.PhoneNumber.Trim(),
            DriversLicenseNumber = request.Request.DriversLicenseNumber.Trim(),
            Status = RiderStatus.PendingApproval
        };

        await riderRepository.AddAsync(rider, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var riderDto = RiderDtoMapper.ToDto(rider);
        return Result<CreateRiderResponse>.Success(new CreateRiderResponse(riderDto));
    }
}
