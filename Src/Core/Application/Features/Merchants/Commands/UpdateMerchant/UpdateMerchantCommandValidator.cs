using FluentValidation;

namespace SwaOlova.Application.Features.Merchants.Commands.UpdateMerchant;

public sealed class UpdateMerchantCommandValidator : AbstractValidator<UpdateMerchantCommand>
{
    public UpdateMerchantCommandValidator()
    {
        RuleFor(x => x.MerchantId).NotEmpty();
        RuleFor(x => x.Request).NotNull();

        When(x => x.Request is not null, () =>
        {
            RuleFor(x => x.Request.Name)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Request.ContactNumber)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(x => x.Request.AddressLine1)
                .NotEmpty()
                .MaximumLength(500);

            RuleFor(x => x.Request.Village)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Request.Latitude)
                .InclusiveBetween(-90m, 90m);

            RuleFor(x => x.Request.Longitude)
                .InclusiveBetween(-180m, 180m);
        });
    }
}
