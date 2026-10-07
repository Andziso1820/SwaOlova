using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Riders.Queries.GetRiderDeliveries;

public sealed record GetRiderDeliveriesQuery(Guid RiderId, int PageNumber = 1, int PageSize = 10)
    : QueryBase<GetRiderDeliveriesResponse>;
