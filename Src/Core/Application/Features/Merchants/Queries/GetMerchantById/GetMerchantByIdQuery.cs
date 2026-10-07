using SwaOlova.Application.Common.Abstractions;
using SwaOlova.Application.Features.Merchants.Dtos;

namespace SwaOlova.Application.Features.Merchants.Queries.GetMerchantById;

public sealed record GetMerchantByIdQuery(Guid MerchantId)
    : QueryBase<MerchantDto>;
