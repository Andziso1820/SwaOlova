using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Merchants.Dtos;
using SwaOlova.Domain.Merchant;

namespace SwaOlova.Application.Features.Merchants.Queries.GetMerchantCategories;

public sealed class GetMerchantCategoriesQueryHandler(IRepository<MerchantCategory> merchantCategoryRepository)
    : IRequestHandler<GetMerchantCategoriesQuery, Result<IReadOnlyCollection<MerchantCategoryLookupDto>>>
{
    public async Task<Result<IReadOnlyCollection<MerchantCategoryLookupDto>>> Handle(
        GetMerchantCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        var categories = await merchantCategoryRepository.GetAllAsync(cancellationToken);

        var lookups = categories
            .OrderBy(category => category.Name)
            .Select(category => new MerchantCategoryLookupDto(
                category.Id,
                category.Name))
            .ToArray();

        return Result<IReadOnlyCollection<MerchantCategoryLookupDto>>.Success(lookups);
    }
}
