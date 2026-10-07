using SwaOlova.Application.Common.Abstractions;
using SwaOlova.Application.Features.Locations.Dtos;

namespace SwaOlova.Application.Features.Locations.Queries.GetCoverageByArea;

public sealed record GetCoverageByAreaQuery(Guid MerchantId, Guid ZoneId)
    : QueryBase<GetCoverageByAreaResponse>;
