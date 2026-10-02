using SwaOlova.Domain.Product;

namespace SwaOlova.Application.Features.Products.Dtos;

internal static class ProductDtoMapper
{
    public static ProductDto ToDto(Product product, Inventory? inventory, IReadOnlyCollection<ProductImage> images)
    {
        var inventoryDto = inventory is not null
            ? new InventoryDto(inventory.Id, inventory.ProductId, inventory.QuantityAvailable, inventory.InStock)
            : null;

        var imagesDto = images
            .Select(img => new ProductImageDto(img.Id, img.ProductId, img.ImageUrl))
            .ToArray();

        return new ProductDto(
            product.Id,
            product.MerchantId,
            product.CategoryId,
            product.Name,
            product.Description,
            product.Price,
            product.PreparationTimeMinutes,
            product.Status,
            inventoryDto,
            imagesDto);
    }

    public static ProductDto ToDto(Product product)
    {
        return new ProductDto(
            product.Id,
            product.MerchantId,
            product.CategoryId,
            product.Name,
            product.Description,
            product.Price,
            product.PreparationTimeMinutes,
            product.Status,
            null,
            Array.Empty<ProductImageDto>());
    }
}
