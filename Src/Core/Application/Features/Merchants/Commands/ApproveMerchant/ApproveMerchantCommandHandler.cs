using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Domain.Enums;
using SwaOlova.Domain.Merchant;

namespace SwaOlova.Application.Features.Merchants.Commands.ApproveMerchant;

public sealed class ApproveMerchantCommandHandler(
    IMerchantRepository merchantRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<ApproveMerchantCommand, Result>
{
    public async Task<Result> Handle(ApproveMerchantCommand request, CancellationToken cancellationToken)
    {
        var merchant = await merchantRepository.GetByIdAsync(request.MerchantId, cancellationToken);
        if (merchant is null)
        {
            return Result.Failure($"Merchant with ID '{request.MerchantId}' was not found.");
        }

        if (merchant.Status != MerchantStatus.PendingApproval)
        {
            return Result.Failure($"Only merchants with status '{MerchantStatus.PendingApproval}' can be approved.");
        }

        merchant.Status = MerchantStatus.Active;

        await merchantRepository.UpdateAsync(merchant, cancellationToken);
        await merchantRepository.AddActivityAsync(new MerchantActivity
        {
            Id = Guid.NewGuid(),
            MerchantId = merchant.Id,
            ActivityType = MerchantActivityType.StatusChanged,
            Title = "Merchant approved",
            Description = $"Merchant status changed from {MerchantStatus.PendingApproval} to {MerchantStatus.Active}."
        }, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
