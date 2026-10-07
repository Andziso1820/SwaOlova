using MediatR;
using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Products.Commands.DeleteProduct;

public sealed record DeleteProductCommand(Guid ProductId)
    : CommandBase<Unit>;
