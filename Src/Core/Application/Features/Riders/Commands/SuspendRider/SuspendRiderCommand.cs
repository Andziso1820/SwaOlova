using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Riders.Commands.SuspendRider;

public sealed record SuspendRiderCommand(Guid RiderId)
    : CommandBase<SuspendRiderResponse>;
