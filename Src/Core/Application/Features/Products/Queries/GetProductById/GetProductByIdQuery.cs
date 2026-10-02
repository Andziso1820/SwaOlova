using SwaOlova.Application.Common.Abstractions;
using SwaOlova.Application.Features.Products.Dtos;

namespace SwaOlova.Application.Features.Products.Queries.GetProductById;

public sealed record GetProductByIdQuery(Guid ProductId)
    : QueryBase<ProductDto>;
