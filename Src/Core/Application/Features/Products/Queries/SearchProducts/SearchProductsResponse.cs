using SwaOlova.Application.Features.Products.Dtos;

namespace SwaOlova.Application.Features.Products.Queries.SearchProducts;

public sealed record SearchProductsResponse(
    IReadOnlyCollection<ProductDto> Products,
    int TotalCount,
    int PageNumber,
    int PageSize);
