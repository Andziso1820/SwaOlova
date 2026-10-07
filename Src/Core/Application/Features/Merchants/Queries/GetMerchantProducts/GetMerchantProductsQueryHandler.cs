using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Merchants.Dtos;

namespace SwaOlova.Application.Features.Merchants.Queries.GetMerchantProducts;

public sealed class GetMerchantProductsQueryHandler(IMerchantRepository merchantRepository)
    : IRequestHandler<GetMerchantProductsQuery, Result<GetMerchantProductsResponse>>
{
    public async Task<Result<GetMerchantProductsResponse>> Handle(GetMerchantProductsQuery request, CancellationToken cancellationToken)
    {
        var merchant = await merchantRepository.GetByIdAsync(request.MerchantId, cancellationToken);
        if (merchant is null)
        {
            return Result<GetMerchantProductsResponse>.Failure($"Merchant with ID '{request.MerchantId}' was not found.");
        }

        var allProducts = merchant.Products.ToList();
        var totalCount = allProducts.Count;

        var skip = (request.PageNumber - 1) * request.PageSize;
        var products = allProducts
            .Skip(skip)
            .Take(request.PageSize)
            .Select(p => new ProductSummaryDto(
                p.Id,
                p.Name,
                p.Price,
                p.PreparationTimeMinutes,
                p.Status.ToString()))
            .ToArray();

        var response = new GetMerchantProductsResponse(products, totalCount, request.PageNumber, request.PageSize);
        return Result<GetMerchantProductsResponse>.Success(response);
    }
}
