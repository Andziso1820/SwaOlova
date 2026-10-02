using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Products.Dtos;
using SwaOlova.Domain.Enums;

namespace SwaOlova.Application.Features.Products.Commands.UpdateProduct;

public sealed class UpdateProductCommandHandler(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateProductCommand, Result<UpdateProductResponse>>
{
    public async Task<Result<UpdateProductResponse>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.ProductId, cancellationToken);
        if (product is null)
        {
            return Result<UpdateProductResponse>.Failure($"Product with ID '{request.ProductId}' was not found.");
        }

        if (product.Status == ProductStatus.Discontinued)
        {
            return Result<UpdateProductResponse>.Failure("Discontinued products cannot be updated.");
        }

        product.Name = request.Request.Name.Trim();
        product.Description = request.Request.Description.Trim();
        product.Price = request.Request.Price;
        product.PreparationTimeMinutes = request.Request.PreparationTimeMinutes;

        await productRepository.UpdateAsync(product, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var productDto = ProductDtoMapper.ToDto(product);
        return Result<UpdateProductResponse>.Success(new UpdateProductResponse(productDto));
    }
}
