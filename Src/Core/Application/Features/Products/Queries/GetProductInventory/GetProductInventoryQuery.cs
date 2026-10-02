using SwaOlova.Application.Common.Abstractions;
using SwaOlova.Application.Features.Products.Dtos;

namespace SwaOlova.Application.Features.Products.Queries.GetProductInventory;

public sealed record GetProductInventoryQuery(Guid ProductId)
    : QueryBase<InventoryDto>;
