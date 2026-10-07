using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Riders.Commands.SetAvailability;

public sealed record SetAvailabilityCommand(Guid RiderId, SetAvailabilityRequest Request)
    : CommandBase<SetAvailabilityResponse>;
