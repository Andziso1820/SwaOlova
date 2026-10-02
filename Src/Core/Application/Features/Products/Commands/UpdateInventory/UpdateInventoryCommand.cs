using MediatR;
using SwaOlova.Application.Common.Abstractions;

namespace SwaOlova.Application.Features.Products.Commands.UpdateInventory;

public sealed record UpdateInventoryCommand(Guid ProductId, UpdateInventoryRequest Request)
    : CommandBase<Unit>;
