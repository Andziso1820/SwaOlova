namespace SwaOlova.Application.Features.Products.Commands.CreateProduct;

public sealed record CreateProductRequest(
    Guid MerchantId,
    Guid CategoryId,
    string Name,
    string Description,
    decimal Price,
    int PreparationTimeMinutes);
