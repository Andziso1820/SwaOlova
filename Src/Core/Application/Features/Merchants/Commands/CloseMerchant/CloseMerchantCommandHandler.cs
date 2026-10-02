using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;
using SwaOlova.Domain.Enums;

namespace SwaOlova.Application.Features.Merchants.Commands.CloseMerchant;

public sealed class CloseMerchantCommandHandler(
    IMerchantRepository merchantRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CloseMerchantCommand, Result>
{
    public async Task<Result> Handle(
        CloseMerchantCommand request,
        CancellationToken cancellationToken)
    {
        var merchant =
            await merchantRepository.GetByIdAsync(
                request.MerchantId,
                cancellationToken);

        if (merchant is null)
        {
            return Result.Failure(
                $"Merchant with ID '{request.MerchantId}' was not found.");
        }

        if (merchant.Status == MerchantStatus.Closed)
        {
            return Result.Failure(
                "Merchant is already closed.");
        }

        merchant.Status = MerchantStatus.Closed;
        //merchant.CloseReason = request.Reason;

        await merchantRepository.UpdateAsync(
            merchant,
            cancellationToken);

        await unitOfWork.SaveChangesAsync(
            cancellationToken);

        return Result.Success();
    }
}