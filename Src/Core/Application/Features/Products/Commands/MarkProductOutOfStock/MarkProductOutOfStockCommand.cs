using MediatR;
using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Products.Commands.MarkProductOutOfStock;

public sealed record MarkProductOutOfStockCommand(Guid ProductId)
    : CommandBase<Unit>;
