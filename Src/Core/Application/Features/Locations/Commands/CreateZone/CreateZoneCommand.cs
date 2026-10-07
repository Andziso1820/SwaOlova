using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Locations.Commands.CreateZone;

public sealed record CreateZoneCommand(CreateZoneRequest Request)
    : CommandBase<CreateZoneResponse>;
