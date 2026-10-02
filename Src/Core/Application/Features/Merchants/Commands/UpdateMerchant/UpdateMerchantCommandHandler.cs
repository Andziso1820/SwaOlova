using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Merchants.Dtos;
using SwaOlova.Domain.Enums;

namespace SwaOlova.Application.Features.Merchants.Commands.UpdateMerchant;

public sealed class UpdateMerchantCommandHandler(
    IMerchantRepository merchantRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateMerchantCommand, Result<UpdateMerchantResponse>>
{
    public async Task<Result<UpdateMerchantResponse>> Handle(UpdateMerchantCommand request, CancellationToken cancellationToken)
    {
        var merchant = await merchantRepository.GetByIdAsync(request.MerchantId, cancellationToken);
        if (merchant is null)
        {
            return Result<UpdateMerchantResponse>.Failure($"Merchant with ID '{request.MerchantId}' was not found.");
        }

        if (merchant.Status == MerchantStatus.Closed)
        {
            return Result<UpdateMerchantResponse>.Failure("Closed merchants cannot be updated.");
        }

        merchant.Name = request.Request.Name.Trim();
        merchant.ContactNumber = request.Request.ContactNumber.Trim();
        merchant.Address.AddressLine1 = request.Request.AddressLine1.Trim();
        merchant.Address.Village = request.Request.Village.Trim();
        merchant.Address.Latitude = request.Request.Latitude;
        merchant.Address.Longitude = request.Request.Longitude;

        await merchantRepository.UpdateAsync(merchant, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var merchantDto = MerchantDtoMapper.ToDto(merchant);
        return Result<UpdateMerchantResponse>.Success(new UpdateMerchantResponse(merchantDto));
    }
}
