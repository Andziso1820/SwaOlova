namespace SwaOlova.Application.Features.Products.Commands.UpdateProduct;

public sealed record UpdateProductRequest(
    string Name,
    string Description,
    decimal Price,
    int PreparationTimeMinutes);
