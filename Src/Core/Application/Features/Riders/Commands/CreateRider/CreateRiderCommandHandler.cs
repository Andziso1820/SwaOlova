using FluentValidation;
using MediatR;
using SwaOlova.Application.Common.Abstractions;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Riders.Common;
using SwaOlova.Application.Features.Riders.Dtos;
using SwaOlova.Domain.Enums;
using SwaOlova.Domain.Rider;

namespace SwaOlova.Application.Features.Riders.Commands.CreateRider;

public sealed record CreateRiderRequest(
    string FirstName,
    string LastName,
    string PhoneNumber,
    string? Email,
    string DriversLicenseNumber);

public sealed record CreateRiderCommand(CreateRiderRequest Request)
    : CommandBase<CreateRiderResponse>;

public sealed class CreateRiderCommandValidator : AbstractValidator<CreateRiderCommand>
{
    public CreateRiderCommandValidator()
    {
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

public sealed class CreateRiderCommandHandler(
    IRiderRepository riderRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateRiderCommand, Result<CreateRiderResponse>>
{
    public async Task<Result<CreateRiderResponse>> Handle(CreateRiderCommand request, CancellationToken cancellationToken)
    {
        var input = request.Request;

        if (await riderRepository.PhoneNumberExistsAsync(input.PhoneNumber, null, cancellationToken))
        {
            return Result<CreateRiderResponse>.Failure("A rider with this phone number already exists.");
        }

        if (await riderRepository.DriversLicenseExistsAsync(input.DriversLicenseNumber, null, cancellationToken))
        {
            return Result<CreateRiderResponse>.Failure("A rider with this driver's license number already exists.");
        }

        var rider = new Rider
        {
            Id = Guid.NewGuid(),
            RiderNumber = $"RID-{DateTime.UtcNow:yyyyMMddHHmmssfff}",
            FirstName = input.FirstName.Trim(),
            LastName = input.LastName.Trim(),
            PhoneNumber = input.PhoneNumber.Trim(),
            Email = string.IsNullOrWhiteSpace(input.Email) ? null : input.Email.Trim(),
            DriversLicenseNumber = input.DriversLicenseNumber.Trim(),
            Status = RiderStatus.PendingApproval
        };

        await riderRepository.AddAsync(rider, cancellationToken);

        await RiderActivityRecorder.RecordAsync(
            riderRepository,
            rider.Id,
            RiderActivityType.Created,
            "Rider registered",
            $"Rider {rider.RiderNumber} registered and awaiting approval.",
            cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<CreateRiderResponse>.Success(new CreateRiderResponse(RiderDtoMapper.ToDto(rider)));
    }
}
