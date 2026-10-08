using FluentValidation;
using MediatR;
using SwaOlova.Application.Common.Abstractions;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Riders.Common;
using SwaOlova.Application.Features.Riders.Dtos;
using SwaOlova.Domain.Enums;
using SwaOlova.Domain.Vehicle;

namespace SwaOlova.Application.Features.Riders.Commands.SaveRiderVehicle;

public sealed record SaveRiderVehicleRequest(
    string RegistrationNumber,
    string VehicleType,
    string Make,
    string Model);

public sealed record SaveRiderVehicleResponse(RiderVehicleDto Vehicle);

public sealed record SaveRiderVehicleCommand(Guid RiderId, SaveRiderVehicleRequest Request)
    : CommandBase<SaveRiderVehicleResponse>;

public sealed class SaveRiderVehicleCommandValidator : AbstractValidator<SaveRiderVehicleCommand>
{
    public SaveRiderVehicleCommandValidator()
    {
        RuleFor(x => x.RiderId).NotEmpty();
        RuleFor(x => x.Request).NotNull();

        When(x => x.Request is not null, () =>
        {
            RuleFor(x => x.Request.RegistrationNumber).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Request.VehicleType).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Request.Make).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Request.Model).NotEmpty().MaximumLength(100);
        });
    }
}

public sealed class SaveRiderVehicleCommandHandler(
    IRiderRepository riderRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<SaveRiderVehicleCommand, Result<SaveRiderVehicleResponse>>
{
    public async Task<Result<SaveRiderVehicleResponse>> Handle(SaveRiderVehicleCommand request, CancellationToken cancellationToken)
    {
        var rider = await riderRepository.GetByIdAsync(request.RiderId, cancellationToken);
        if (rider is null)
        {
            return Result<SaveRiderVehicleResponse>.Failure($"Rider with ID '{request.RiderId}' was not found.");
        }

        var input = request.Request;
        var registration = input.RegistrationNumber.Trim().ToUpperInvariant();

        if (await riderRepository.RegistrationNumberExistsAsync(registration, rider.Id, cancellationToken))
        {
            return Result<SaveRiderVehicleResponse>.Failure("This registration number is already assigned to another rider.");
        }

        var vehicle = await riderRepository.GetVehicleAsync(rider.Id, cancellationToken);
        var isNew = vehicle is null;

        vehicle ??= new Vehicle { Id = Guid.NewGuid(), RiderId = rider.Id };
        vehicle.RegistrationNumber = registration;
        vehicle.VehicleType = input.VehicleType.Trim();
        vehicle.Make = input.Make.Trim();
        vehicle.Model = input.Model.Trim();

        if (isNew)
        {
            await riderRepository.AddVehicleAsync(vehicle, cancellationToken);
        }
        else
        {
            vehicle.ModifiedDate = DateTime.UtcNow;
        }

        await RiderActivityRecorder.RecordAsync(
            riderRepository,
            rider.Id,
            RiderActivityType.VehicleUpdated,
            isNew ? "Vehicle added" : "Vehicle updated",
            $"{vehicle.VehicleType} {vehicle.Make} {vehicle.Model} ({vehicle.RegistrationNumber}).",
            cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<SaveRiderVehicleResponse>.Success(new SaveRiderVehicleResponse(RiderDtoMapper.ToVehicleDto(vehicle)));
    }
}
