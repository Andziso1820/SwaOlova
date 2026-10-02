using SwaOlova.Application.Features.Merchants.Dtos;

namespace SwaOlova.Application.Features.Merchants.Queries.SearchMerchants;

public sealed record SearchMerchantsResponse(
    IReadOnlyCollection<MerchantDto> Merchants,
    int TotalCount,
    int PageNumber,
    int PageSize);
