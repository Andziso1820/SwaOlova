using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Riders.Queries.GetRiderLocation;

public sealed record GetRiderLocationQuery(Guid RiderId)
    : QueryBase<GetRiderLocationResponse>;
