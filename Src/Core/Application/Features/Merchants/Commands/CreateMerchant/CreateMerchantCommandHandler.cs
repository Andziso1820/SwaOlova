using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Application.Features.Merchants.Dtos;
using SwaOlova.Domain.Enums;
using SwaOlova.Domain.Merchant;
using MerchantAddress = SwaOlova.Domain.Merchant.MerchantAddress;

namespace SwaOlova.Application.Features.Merchants.Commands.CreateMerchant;

public sealed class CreateMerchantCommandHandler(
    IMerchantRepository merchantRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateMerchantCommand, Result<CreateMerchantResponse>>
{
    public async Task<Result<CreateMerchantResponse>> Handle(CreateMerchantCommand request, CancellationToken cancellationToken)
    {
        var address = new MerchantAddress
        {
            Id = Guid.NewGuid(),
            AddressLine1 = request.Request.AddressLine1.Trim(),
            Village = request.Request.Village.Trim(),
            Latitude = request.Request.Latitude,
            Longitude = request.Request.Longitude
        };

        var merchant = new Merchant
        {
            Id = Guid.NewGuid(),
            MerchantCode = $"MCH-{DateTime.UtcNow:yyyyMMddHHmmssfff}",
            Name = request.Request.Name.Trim(),
            ContactNumber = request.Request.ContactNumber.Trim(),
            Status = MerchantStatus.PendingApproval,
            MerchantCategoryId = request.Request.MerchantCategoryId,
            Address = address
        };

        await merchantRepository.AddAsync(merchant, cancellationToken);
        await merchantRepository.AddActivityAsync(new MerchantActivity
        {
            Id = Guid.NewGuid(),
            MerchantId = merchant.Id,
            ActivityType = MerchantActivityType.Created,
            Title = "Merchant created",
            Description = $"Merchant '{merchant.Name}' was created and placed in Pending Approval."
        }, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var merchantDto = MerchantDtoMapper.ToDto(merchant);
        return Result<CreateMerchantResponse>.Success(new CreateMerchantResponse(merchantDto));
    }
}
