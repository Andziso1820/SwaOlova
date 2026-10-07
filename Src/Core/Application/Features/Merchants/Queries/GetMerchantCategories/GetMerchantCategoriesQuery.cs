using SwaOlova.Application.Common.Abstractions;
using SwaOlova.Application.Features.Merchants.Dtos;

namespace SwaOlova.Application.Features.Merchants.Queries.GetMerchantCategories;

public sealed record GetMerchantCategoriesQuery
    : QueryBase<IReadOnlyCollection<MerchantCategoryLookupDto>>;
