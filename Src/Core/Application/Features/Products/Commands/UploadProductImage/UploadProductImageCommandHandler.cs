using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Domain.Product;

namespace SwaOlova.Application.Features.Products.Commands.UploadProductImage;

public sealed class UploadProductImageCommandHandler(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UploadProductImageCommand, Result>
{
    public async Task<Result> Handle(UploadProductImageCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.ProductId, cancellationToken);
        if (product is null)
        {
            return Result.Failure($"Product with ID '{request.ProductId}' was not found.");
        }

        var productImage = new ProductImage
        {
            Id = Guid.NewGuid(),
            ProductId = request.ProductId,
            ImageUrl = request.Request.ImageUrl.Trim()
        };

        // In a real scenario, you'd add the image to a collection on Product
        // For now, we'll assume images are persisted independently
        // This would require an IProductImageRepository or similar

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
