using FluentValidation;

namespace SwaOlova.Application.Features.Locations.Commands.ConfigureCoverage;

public sealed class ConfigureCoverageCommandValidator : AbstractValidator<ConfigureCoverageCommand>
{
    public ConfigureCoverageCommandValidator()
    {
        RuleFor(x => x.Request).NotNull();

        When(x => x.Request is not null, () =>
        {
            RuleFor(x => x.Request.MerchantId).NotEmpty();

            RuleFor(x => x.Request.ZoneId).NotEmpty();

            RuleFor(x => x.Request.DeliveryFee)
                .GreaterThanOrEqualTo(0m);

            RuleFor(x => x.Request.DeliveryTimeMinutes)
                .GreaterThan(0);
        });
    }
}
