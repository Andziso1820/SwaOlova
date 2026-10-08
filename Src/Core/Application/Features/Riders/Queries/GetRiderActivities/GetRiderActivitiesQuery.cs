using MediatR;
using SwaOlova.Application.Common.Abstractions;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Riders.Dtos;

namespace SwaOlova.Application.Features.Riders.Queries.GetRiderActivities;

public sealed record GetRiderActivitiesQuery(Guid RiderId)
    : QueryBase<IReadOnlyCollection<RiderActivityDto>>;

public sealed class GetRiderActivitiesQueryHandler(IRiderRepository riderRepository)
    : IRequestHandler<GetRiderActivitiesQuery, Result<IReadOnlyCollection<RiderActivityDto>>>
{
    public async Task<Result<IReadOnlyCollection<RiderActivityDto>>> Handle(GetRiderActivitiesQuery request, CancellationToken cancellationToken)
    {
        var activities = await riderRepository.GetActivityHistoryAsync(request.RiderId, cancellationToken);

        return Result<IReadOnlyCollection<RiderActivityDto>>.Success(
            activities.Select(RiderDtoMapper.ToActivityDto).ToArray());
    }
}
