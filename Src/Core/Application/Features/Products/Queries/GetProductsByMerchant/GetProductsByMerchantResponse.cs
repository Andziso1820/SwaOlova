using SwaOlova.Application.Features.Products.Dtos;

namespace SwaOlova.Application.Features.Products.Queries.GetProductsByMerchant;

public sealed record GetProductsByMerchantResponse(
    IReadOnlyCollection<ProductDto> Products,
    int TotalCount,
    int PageNumber,
    int PageSize);
