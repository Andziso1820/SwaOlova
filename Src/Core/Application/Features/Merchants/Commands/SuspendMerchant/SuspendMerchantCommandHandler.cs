using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Domain.Enums;
using SwaOlova.Domain.Merchant;

namespace SwaOlova.Application.Features.Merchants.Commands.SuspendMerchant;

public sealed class SuspendMerchantCommandHandler(
    IMerchantRepository merchantRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<SuspendMerchantCommand, Result>
{
    public async Task<Result> Handle(SuspendMerchantCommand request, CancellationToken cancellationToken)
    {
        var merchant = await merchantRepository.GetByIdAsync(request.MerchantId, cancellationToken);
        if (merchant is null)
        {
            return Result.Failure($"Merchant with ID '{request.MerchantId}' was not found.");
        }

        if (merchant.Status is MerchantStatus.Closed or MerchantStatus.PendingApproval)
        {
            return Result.Failure($"Only active merchants can be suspended.");
        }

        merchant.Status = MerchantStatus.Suspended;
        //merchant.SuspendReason = request.Reason;

        await merchantRepository.UpdateAsync(merchant, cancellationToken);
        await merchantRepository.AddActivityAsync(new MerchantActivity
        {
            Id = Guid.NewGuid(),
            MerchantId = merchant.Id,
            ActivityType = MerchantActivityType.StatusChanged,
            Title = "Merchant suspended",
            Description = $"Merchant status changed from {MerchantStatus.Active} to {MerchantStatus.Suspended}."
        }, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
