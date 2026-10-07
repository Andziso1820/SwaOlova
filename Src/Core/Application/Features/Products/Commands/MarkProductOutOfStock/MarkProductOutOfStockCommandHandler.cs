using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Domain.Enums;

namespace SwaOlova.Application.Features.Products.Commands.MarkProductOutOfStock;

public sealed class MarkProductOutOfStockCommandHandler(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<MarkProductOutOfStockCommand, Result>
{
    public async Task<Result> Handle(MarkProductOutOfStockCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.ProductId, cancellationToken);
        if (product is null)
        {
            return Result.Failure($"Product with ID '{request.ProductId}' was not found.");
        }

        if (product.Status == ProductStatus.Discontinued)
        {
            return Result.Failure("Discontinued products cannot be marked out of stock.");
        }

        product.Status = ProductStatus.OutOfStock;

        await productRepository.UpdateAsync(product, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
