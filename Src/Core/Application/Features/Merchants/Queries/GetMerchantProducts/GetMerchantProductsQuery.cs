using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Merchants.Queries.GetMerchantProducts;

public sealed record GetMerchantProductsQuery(Guid MerchantId, int PageNumber = 1, int PageSize = 10)
    : QueryBase<GetMerchantProductsResponse>;
