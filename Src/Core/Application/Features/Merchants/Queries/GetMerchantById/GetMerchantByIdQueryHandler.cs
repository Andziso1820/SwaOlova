using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Merchants.Dtos;

namespace SwaOlova.Application.Features.Merchants.Queries.GetMerchantById;

public sealed class GetMerchantByIdQueryHandler(IMerchantRepository merchantRepository)
    : IRequestHandler<GetMerchantByIdQuery, Result<MerchantDto>>
{
    public async Task<Result<MerchantDto>> Handle(GetMerchantByIdQuery request, CancellationToken cancellationToken)
    {
        var merchant = await merchantRepository.GetByIdAsync(request.MerchantId, cancellationToken);
        if (merchant is null)
        {
            return Result<MerchantDto>.Failure($"Merchant with ID '{request.MerchantId}' was not found.");
        }

        var merchantDto = MerchantDtoMapper.ToDto(merchant);
        return Result<MerchantDto>.Success(merchantDto);
    }
}
