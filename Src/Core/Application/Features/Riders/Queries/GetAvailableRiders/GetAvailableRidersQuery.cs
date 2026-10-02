using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Riders.Queries.GetAvailableRiders;

public sealed record GetAvailableRidersQuery(int PageNumber = 1, int PageSize = 10)
    : QueryBase<GetAvailableRidersResponse>;
