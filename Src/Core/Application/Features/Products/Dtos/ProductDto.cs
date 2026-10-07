using SwaOlova.Domain.Enums;

namespace SwaOlova.Application.Features.Products.Dtos;

public sealed record InventoryDto(
    Guid Id,
    Guid ProductId,
    int QuantityAvailable,
    bool InStock);

public sealed record ProductImageDto(
    Guid Id,
    Guid ProductId,
    string ImageUrl);

public sealed record ProductDto(
    Guid Id,
    Guid MerchantId,
    Guid CategoryId,
    string Name,
    string Description,
    decimal Price,
    int PreparationTimeMinutes,
    ProductStatus Status,
    InventoryDto? Inventory,
    IReadOnlyCollection<ProductImageDto> Images);
