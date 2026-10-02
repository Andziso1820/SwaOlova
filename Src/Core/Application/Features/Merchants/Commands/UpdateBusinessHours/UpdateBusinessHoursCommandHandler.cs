using MediatR;
using SwaOlova.Application.Common.Interfaces.Repositories;
using SwaOlova.Application.Common.Models;

namespace SwaOlova.Application.Features.Merchants.Commands.UpdateBusinessHours;

public sealed class UpdateBusinessHoursCommandHandler(
    IMerchantRepository merchantRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateBusinessHoursCommand, Result>
{
    public async Task<Result> Handle(UpdateBusinessHoursCommand request, CancellationToken cancellationToken)
    {
        var merchant = await merchantRepository.GetByIdAsync(request.MerchantId, cancellationToken);
        if (merchant is null)
        {
            return Result.Failure($"Merchant with ID '{request.MerchantId}' was not found.");
        }

        // Business hours would typically be stored on the merchant entity or in a separate MerchantBusinessHours table
        // For now, we'll treat this as a placeholder until the domain model is extended
        // In a real scenario, you would update merchant.BusinessHours or persist to a related entity

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
