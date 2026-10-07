using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Merchants.Dtos;

namespace SwaOlova.Application.Features.Merchants.Queries.GetMerchantActivities;

public sealed class GetMerchantActivitiesQueryHandler(IMerchantRepository merchantRepository)
    : IRequestHandler<GetMerchantActivitiesQuery, Result<IReadOnlyCollection<MerchantActivityDto>>>
{
    public async Task<Result<IReadOnlyCollection<MerchantActivityDto>>> Handle(
        GetMerchantActivitiesQuery request,
        CancellationToken cancellationToken)
    {
        var activities = await merchantRepository.GetActivityHistoryAsync(request.MerchantId, cancellationToken);

        var items = activities
            .OrderByDescending(activity => activity.CreatedDate)
            .Select(activity => new MerchantActivityDto(
                activity.CreatedDate == default ? DateTime.UtcNow : activity.CreatedDate,
                activity.Title,
                activity.Description))
            .ToArray();

        return Result<IReadOnlyCollection<MerchantActivityDto>>.Success(items);
    }
}
