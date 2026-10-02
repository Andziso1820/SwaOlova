using FluentValidation;

namespace SwaOlova.Application.Features.Merchants.Commands.ApproveMerchant;

public sealed class ApproveMerchantCommandValidator : AbstractValidator<ApproveMerchantCommand>
{
    public ApproveMerchantCommandValidator()
    {
        RuleFor(x => x.MerchantId).NotEmpty();
    }
}
