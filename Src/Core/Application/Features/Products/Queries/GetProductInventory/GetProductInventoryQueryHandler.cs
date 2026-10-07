using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Products.Dtos;

namespace SwaOlova.Application.Features.Products.Queries.GetProductInventory;

public sealed class GetProductInventoryQueryHandler(IProductRepository productRepository)
    : IRequestHandler<GetProductInventoryQuery, Result<InventoryDto>>
{
    public async Task<Result<InventoryDto>> Handle(GetProductInventoryQuery request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.ProductId, cancellationToken);
        if (product is null)
        {
            return Result<InventoryDto>.Failure($"Product with ID '{request.ProductId}' was not found.");
        }

        // In a real scenario, inventory would be fetched from a repository
        // For now, returning a placeholder
        var inventoryDto = new InventoryDto(
            Guid.NewGuid(),
            request.ProductId,
            0,
            false);

        return Result<InventoryDto>.Success(inventoryDto);
    }
}
