using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Merchants.Dtos;
using SwaOlova.Domain.Enums;
using SwaOlova.Domain.Merchant;

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

        var name = request.Request.Name.Trim();
        var contactNumber = request.Request.ContactNumber.Trim();
        var addressLine1 = request.Request.AddressLine1.Trim();
        var village = request.Request.Village.Trim();

        var changedFields = new List<string>();

        if (!string.Equals(merchant.Name, name, StringComparison.Ordinal))
        {
            changedFields.Add("name");
        }

        if (!string.Equals(merchant.ContactNumber, contactNumber, StringComparison.Ordinal))
        {
            changedFields.Add("contact number");
        }

        if (!string.Equals(merchant.Address.AddressLine1, addressLine1, StringComparison.Ordinal))
        {
            changedFields.Add("address line");
        }

        if (!string.Equals(merchant.Address.Village, village, StringComparison.Ordinal))
        {
            changedFields.Add("village");
        }

        if (merchant.Address.Latitude != request.Request.Latitude)
        {
            changedFields.Add("latitude");
        }

        if (merchant.Address.Longitude != request.Request.Longitude)
        {
            changedFields.Add("longitude");
        }

        merchant.Name = name;
        merchant.ContactNumber = contactNumber;
        merchant.Address.AddressLine1 = addressLine1;
        merchant.Address.Village = village;
        merchant.Address.Latitude = request.Request.Latitude;
        merchant.Address.Longitude = request.Request.Longitude;

        await merchantRepository.UpdateAsync(merchant, cancellationToken);
        await merchantRepository.AddActivityAsync(new MerchantActivity
        {
            Id = Guid.NewGuid(),
            MerchantId = merchant.Id,
            ActivityType = MerchantActivityType.Updated,
            Title = "Merchant details updated",
            Description = changedFields.Count == 0
                ? "Merchant details were saved without any detected field changes."
                : $"Updated fields: {string.Join(", ", changedFields)}."
        }, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var merchantDto = MerchantDtoMapper.ToDto(merchant);
        return Result<UpdateMerchantResponse>.Success(new UpdateMerchantResponse(merchantDto));
    }
}
