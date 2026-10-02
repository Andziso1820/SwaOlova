using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Domain.Enums;

namespace SwaOlova.Application.Features.Merchants.Commands.ActivateMerchant;

public sealed class ActivateMerchantCommandHandler(
    IMerchantRepository merchantRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<ActivateMerchantCommand, Result>
{
    public async Task<Result> Handle(ActivateMerchantCommand request, CancellationToken cancellationToken)
    {
        var merchant = await merchantRepository.GetByIdAsync(request.MerchantId, cancellationToken);
        if (merchant is null)
        {
            return Result.Failure($"Merchant with ID '{request.MerchantId}' was not found.");
        }

        if (merchant.Status != MerchantStatus.Suspended)
        {
            return Result.Failure($"Only suspended merchants can be activated.");
        }

        merchant.Status = MerchantStatus.Active;
        //merchant.ActivateReason = request.Reason;

        await merchantRepository.UpdateAsync(merchant, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
