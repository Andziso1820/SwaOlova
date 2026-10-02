using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Domain.Product;

namespace SwaOlova.Application.Features.Products.Commands.UpdateInventory;

public sealed class UpdateInventoryCommandHandler(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateInventoryCommand, Result>
{
    public async Task<Result> Handle(UpdateInventoryCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.ProductId, cancellationToken);
        if (product is null)
        {
            return Result.Failure($"Product with ID '{request.ProductId}' was not found.");
        }

        // In a real scenario, inventory would be managed through a repository
        // For now, this is creating/updating inventory in memory
        var inventory = new Inventory
        {
            Id = Guid.NewGuid(),
            ProductId = request.ProductId,
            QuantityAvailable = request.Request.QuantityAvailable,
            InStock = request.Request.QuantityAvailable > 0
        };

        // This would need proper persistence logic
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
