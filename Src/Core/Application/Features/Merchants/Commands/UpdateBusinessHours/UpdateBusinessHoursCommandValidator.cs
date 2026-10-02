using FluentValidation;

namespace SwaOlova.Application.Features.Merchants.Commands.UpdateBusinessHours;

public sealed class UpdateBusinessHoursCommandValidator : AbstractValidator<UpdateBusinessHoursCommand>
{
    public UpdateBusinessHoursCommandValidator()
    {
        RuleFor(x => x.MerchantId).NotEmpty();
        RuleFor(x => x.Request).NotNull();

        When(x => x.Request is not null, () =>
        {
            RuleFor(x => x.Request.Hours)
                .NotEmpty()
                .Must(h => h.Count <= 7).WithMessage("Business hours can have at most 7 days.");

            RuleForEach(x => x.Request.Hours)
                .ChildRules(rules =>
                {
                    rules.RuleFor(d => d.Day)
                        .NotEmpty()
                        .MaximumLength(20);

                    rules.RuleFor(d => d)
                        .Custom((day, context) =>
                        {
                            if (!day.IsClosed && day.OpeningTime >= day.ClosingTime)
                            {
                                context.AddFailure("Opening time must be before closing time.");
                            }
                        });
                });
        });
    }
}
