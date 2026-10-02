using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Products.Queries.SearchProducts;

public sealed record SearchProductsQuery(string SearchTerm, int PageNumber = 1, int PageSize = 10)
    : QueryBase<SearchProductsResponse>;
