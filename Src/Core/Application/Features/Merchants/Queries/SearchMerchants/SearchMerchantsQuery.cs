using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Merchants.Queries.SearchMerchants;

public sealed record SearchMerchantsQuery(string SearchTerm, int PageNumber = 1, int PageSize = 10)
    : QueryBase<SearchMerchantsResponse>;
