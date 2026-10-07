using SwaOlova.Application.Common.Abstractions;
using SwaOlova.Application.Features.Locations.Dtos;

namespace SwaOlova.Application.Features.Locations.Queries.GetVillageById;

public sealed record GetVillageByIdQuery(Guid VillageId)
    : QueryBase<VillageDto>;
