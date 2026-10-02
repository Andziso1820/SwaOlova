using FluentValidation;

namespace SwaOlova.Application.Features.Merchants.Commands.CloseMerchant;

public sealed class CloseMerchantCommandValidator
    : AbstractValidator<CloseMerchantCommand>
{
    public CloseMerchantCommandValidator()
    {
        RuleFor(x => x.MerchantId)
            .NotEmpty();

        RuleFor(x => x.Reason)
            .NotEmpty()
            .MaximumLength(500);
    }
}