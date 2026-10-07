using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Products.Dtos;
using SwaOlova.Domain.Enums;
using SwaOlova.Domain.Product;

namespace SwaOlova.Application.Features.Products.Commands.CreateProduct;

public sealed class CreateProductCommandHandler(
    IProductRepository productRepository,
    IMerchantRepository merchantRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateProductCommand, Result<CreateProductResponse>>
{
    public async Task<Result<CreateProductResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        // Verify merchant exists
        var merchant = await merchantRepository.GetByIdAsync(request.Request.MerchantId, cancellationToken);
        if (merchant is null)
        {
            return Result<CreateProductResponse>.Failure($"Merchant with ID '{request.Request.MerchantId}' was not found.");
        }

        var product = new Product
        {
            Id = Guid.NewGuid(),
            MerchantId = request.Request.MerchantId,
            CategoryId = request.Request.CategoryId,
            Name = request.Request.Name.Trim(),
            Description = request.Request.Description.Trim(),
            Price = request.Request.Price,
            PreparationTimeMinutes = request.Request.PreparationTimeMinutes,
            Status = ProductStatus.Active
        };

        await productRepository.AddAsync(product, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var productDto = ProductDtoMapper.ToDto(product);
        return Result<CreateProductResponse>.Success(new CreateProductResponse(productDto));
    }
}
