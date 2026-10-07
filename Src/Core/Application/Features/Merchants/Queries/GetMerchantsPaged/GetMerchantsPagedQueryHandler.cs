using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Merchants.Dtos;

namespace SwaOlova.Application.Features.Merchants.Queries.GetMerchantsPaged;

public sealed class GetMerchantsPagedQueryHandler(IMerchantRepository merchantRepository)
    : IRequestHandler<GetMerchantsPagedQuery, Result<PagedResult<MerchantDto>>>
{
    public async Task<Result<PagedResult<MerchantDto>>> Handle(GetMerchantsPagedQuery request, CancellationToken cancellationToken)
    {
        // Get all merchants (ideally this would use a pagedbepository method)
        var allMerchants = await merchantRepository.GetAllAsync(cancellationToken);
        var totalCount = allMerchants.Count;

        var skip = (request.PageNumber - 1) * request.PageSize;
        var merchants = allMerchants
            .Skip(skip)
            .Take(request.PageSize)
            .Select(MerchantDtoMapper.ToDto)
            .ToArray();

        var pagedResult = PagedResult<MerchantDto>.Create(
            merchants,
            totalCount,
            request.PageNumber,
            request.PageSize);

        return Result<PagedResult<MerchantDto>>.Success(pagedResult);
    }
}
