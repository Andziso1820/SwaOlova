using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Locations.Commands.UpdateVillage;

public sealed record UpdateVillageCommand(Guid VillageId, UpdateVillageRequest Request)
    : CommandBase<UpdateVillageResponse>;
