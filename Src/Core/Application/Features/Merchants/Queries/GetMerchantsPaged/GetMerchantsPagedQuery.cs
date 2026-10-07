using SwaOlova.Application.Common.Abstractions;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Merchants.Dtos;

namespace SwaOlova.Application.Features.Merchants.Queries.GetMerchantsPaged;

public sealed record GetMerchantsPagedQuery(int PageNumber = 1, int PageSize = 10)
    : QueryBase<PagedResult<MerchantDto>>;
