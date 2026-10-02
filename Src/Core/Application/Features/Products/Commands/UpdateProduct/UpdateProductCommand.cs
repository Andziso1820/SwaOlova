using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Products.Commands.UpdateProduct;

public sealed record UpdateProductCommand(Guid ProductId, UpdateProductRequest Request)
    : CommandBase<UpdateProductResponse>;
