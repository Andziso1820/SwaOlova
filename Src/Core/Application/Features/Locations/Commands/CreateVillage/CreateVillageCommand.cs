using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Locations.Commands.CreateVillage;

public sealed record CreateVillageCommand(CreateVillageRequest Request)
    : CommandBase<CreateVillageResponse>;
