using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Products.Commands.CreateProduct;

public sealed record CreateProductCommand(CreateProductRequest Request)
    : CommandBase<CreateProductResponse>;
