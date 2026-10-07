using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Riders.Queries.GetRiderById;

public sealed record GetRiderByIdQuery(Guid RiderId)
    : QueryBase<GetRiderByIdResponse>;
