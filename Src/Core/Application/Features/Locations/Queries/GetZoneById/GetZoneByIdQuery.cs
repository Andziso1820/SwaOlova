using SwaOlova.Application.Common.Abstractions;
using SwaOlova.Application.Features.Locations.Dtos;

namespace SwaOlova.Application.Features.Locations.Queries.GetZoneById;

public sealed record GetZoneByIdQuery(Guid ZoneId)
    : QueryBase<ZoneDto>;
