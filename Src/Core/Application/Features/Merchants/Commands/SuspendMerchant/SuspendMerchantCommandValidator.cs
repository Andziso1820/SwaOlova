using FluentValidation;

namespace SwaOlova.Application.Features.Merchants.Commands.SuspendMerchant;

public sealed class SuspendMerchantCommandValidator : AbstractValidator<SuspendMerchantCommand>
{
    public SuspendMerchantCommandValidator()
    {
        RuleFor(x => x.MerchantId).NotEmpty();
        RuleFor(x => x.Reason).NotEmpty();
    }
}
