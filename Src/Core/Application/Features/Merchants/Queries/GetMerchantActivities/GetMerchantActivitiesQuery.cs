using SwaOlova.Application.Common.Abstractions;
using SwaOlova.Application.Features.Merchants.Dtos;

namespace SwaOlova.Application.Features.Merchants.Queries.GetMerchantActivities;

public sealed record GetMerchantActivitiesQuery(Guid MerchantId)
    : QueryBase<IReadOnlyCollection<MerchantActivityDto>>;
