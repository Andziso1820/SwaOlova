using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Products.Dtos;

namespace SwaOlova.Application.Features.Products.Queries.GetProductById;

public sealed class GetProductByIdQueryHandler(IProductRepository productRepository)
    : IRequestHandler<GetProductByIdQuery, Result<ProductDto>>
{
    public async Task<Result<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.ProductId, cancellationToken);
        if (product is null)
        {
            return Result<ProductDto>.Failure($"Product with ID '{request.ProductId}' was not found.");
        }

        var productDto = ProductDtoMapper.ToDto(product);
        return Result<ProductDto>.Success(productDto);
    }
}
