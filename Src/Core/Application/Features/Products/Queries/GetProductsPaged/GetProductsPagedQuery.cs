using SwaOlova.Application.Common.Abstractions;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Products.Dtos;

namespace SwaOlova.Application.Features.Products.Queries.GetProductsPaged;

public sealed record GetProductsPagedQuery(int PageNumber = 1, int PageSize = 10)
    : QueryBase<PagedResult<ProductDto>>;
