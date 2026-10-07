using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Riders.Commands.CreateRider;

public sealed record CreateRiderCommand(CreateRiderRequest Request)
    : CommandBase<CreateRiderResponse>;
