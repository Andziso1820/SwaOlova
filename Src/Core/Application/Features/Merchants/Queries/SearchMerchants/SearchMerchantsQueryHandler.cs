using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Merchants.Dtos;

namespace SwaOlova.Application.Features.Merchants.Queries.SearchMerchants;

public sealed class SearchMerchantsQueryHandler(IMerchantRepository merchantRepository)
    : IRequestHandler<SearchMerchantsQuery, Result<SearchMerchantsResponse>>
{
    public async Task<Result<SearchMerchantsResponse>> Handle(SearchMerchantsQuery request, CancellationToken cancellationToken)
    {
        // Get all merchants (ideally this would be done via a repository method that handles search/paging)
        // For now, filtering in memory
        var term = request.SearchTerm.ToLowerInvariant();
        var skip = (request.PageNumber - 1) * request.PageSize;

        // This would ideally use repository filtering to avoid loading all merchants into memory
        var allMerchants = await merchantRepository.GetAllAsync(cancellationToken);

        var filtered = allMerchants
            .Where(m => m.Name.ToLowerInvariant().Contains(term) ||
                       m.MerchantCode.ToLowerInvariant().Contains(term))
            .ToList();

        var totalCount = filtered.Count;
        var merchants = filtered
            .Skip(skip)
            .Take(request.PageSize)
            .Select(MerchantDtoMapper.ToDto)
            .ToArray();

        var response = new SearchMerchantsResponse(merchants, totalCount, request.PageNumber, request.PageSize);
        return Result<SearchMerchantsResponse>.Success(response);
    }
}
