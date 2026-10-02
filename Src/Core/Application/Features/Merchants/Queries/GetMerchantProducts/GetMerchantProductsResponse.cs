using SwaOlova.Application.Features.Merchants.Dtos;

namespace SwaOlova.Application.Features.Merchants.Queries.GetMerchantProducts;

public sealed record GetMerchantProductsResponse(
    IReadOnlyCollection<ProductSummaryDto> Products,
    int TotalCount,
    int PageNumber,
    int PageSize);
