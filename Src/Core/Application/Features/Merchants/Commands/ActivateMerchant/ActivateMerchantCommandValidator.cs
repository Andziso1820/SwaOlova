using FluentValidation;

namespace SwaOlova.Application.Features.Merchants.Commands.ActivateMerchant;

public sealed class ActivateMerchantCommandValidator : AbstractValidator<ActivateMerchantCommand>
{
    public ActivateMerchantCommandValidator()
    {
        RuleFor(x => x.MerchantId).NotEmpty();
        RuleFor(x => x.Reason).NotEmpty();
    }
}
