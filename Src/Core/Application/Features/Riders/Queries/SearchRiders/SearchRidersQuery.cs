using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Riders.Queries.SearchRiders;

public sealed record SearchRidersQuery(SearchRidersRequest Request)
    : QueryBase<SearchRidersResponse>;
