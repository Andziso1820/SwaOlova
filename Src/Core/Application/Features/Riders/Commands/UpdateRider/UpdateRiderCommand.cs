using FluentValidation;
using MediatR;
using SwaOlova.Application.Common.Abstractions;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Riders.Common;
using SwaOlova.Application.Features.Riders.Dtos;
using SwaOlova.Domain.Enums;

namespace SwaOlova.Application.Features.Riders.Commands.UpdateRider;

public sealed record UpdateRiderRequest(
    string FirstName,
    string LastName,
    string PhoneNumber,
    string? Email,
    string DriversLicenseNumber);

public sealed record UpdateRiderResponse(RiderDto Rider);

public sealed record UpdateRiderCommand(Guid RiderId, UpdateRiderRequest Request)
    : CommandBase<UpdateRiderResponse>;

public sealed class UpdateRiderCommandValidator : AbstractValidator<UpdateRiderCommand>
{
    public UpdateRiderCommandValidator()
    {
        RuleFor(x => x.RiderId).NotEmpty();
        RuleFor(x => x.Request).NotNull();

        When(x => x.Request is not null, () =>
        {
            RuleFor(x => x.Request.FirstName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Request.LastName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Request.PhoneNumber).NotEmpty().MaximumLength(20);
            RuleFor(x => x.Request.DriversLicenseNumber).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Request.Email)
                .MaximumLength(256)
                .EmailAddress()
                .When(x => !string.IsNullOrWhiteSpace(x.Request.Email));
        });
    }
}

public sealed class UpdateRiderCommandHandler(
    IRiderRepository riderRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateRiderCommand, Result<UpdateRiderResponse>>
{
    public async Task<Result<UpdateRiderResponse>> Handle(UpdateRiderCommand request, CancellationToken cancellationToken)
    {
        var rider = await riderRepository.GetByIdAsync(request.RiderId, cancellationToken);
        if (rider is null)
        {
            return Result<UpdateRiderResponse>.Failure($"Rider with ID '{request.RiderId}' was not found.");
        }

        var input = request.Request;

        if (await riderRepository.PhoneNumberExistsAsync(input.PhoneNumber, rider.Id, cancellationToken))
        {
            return Result<UpdateRiderResponse>.Failure("Another rider already uses this phone number.");
        }

        if (await riderRepository.DriversLicenseExistsAsync(input.DriversLicenseNumber, rider.Id, cancellationToken))
        {
            return Result<UpdateRiderResponse>.Failure("Another rider already uses this driver's license number.");
        }

        rider.FirstName = input.FirstName.Trim();
        rider.LastName = input.LastName.Trim();
        rider.PhoneNumber = input.PhoneNumber.Trim();
        rider.Email = string.IsNullOrWhiteSpace(input.Email) ? null : input.Email.Trim();
        rider.DriversLicenseNumber = input.DriversLicenseNumber.Trim();

        await riderRepository.UpdateAsync(rider, cancellationToken);

        await RiderActivityRecorder.RecordAsync(
            riderRepository,
            rider.Id,
            RiderActivityType.Updated,
            "Rider details updated",
            "Personal and contact details were updated.",
            cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<UpdateRiderResponse>.Success(new UpdateRiderResponse(RiderDtoMapper.ToDto(rider)));
    }
}
