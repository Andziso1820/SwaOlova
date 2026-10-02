using FluentValidation;

namespace SwaOlova.Application.Features.Merchants.Commands.AddComplianceDocument;

public sealed class AddComplianceDocumentCommandValidator : AbstractValidator<AddComplianceDocumentCommand>
{
    public AddComplianceDocumentCommandValidator()
    {
        RuleFor(x => x.MerchantId)
            .NotEmpty();

        RuleFor(x => x.Request)
            .NotNull();

        When(x => x.Request is not null, () =>
        {
            RuleFor(x => x.Request.Name)
                .NotEmpty()
                .MaximumLength(250);

            RuleFor(x => x.Request.FileUrl)
                .NotEmpty()
                .MaximumLength(500)
                .Must(value => Uri.TryCreate(value, UriKind.Absolute, out _))
                .WithMessage("Document URL must be a valid absolute URL.");
        });
    }
}
