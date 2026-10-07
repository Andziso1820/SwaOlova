using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Products.Queries.GetProductsByMerchant;

public sealed record GetProductsByMerchantQuery(Guid MerchantId, int PageNumber = 1, int PageSize = 10)
    : QueryBase<GetProductsByMerchantResponse>;
