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
            RuleFor(x => x.Request.StoredFileName)
                .NotEmpty()
                .MaximumLength(260);

            RuleFor(x => x.Request.ContentType)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Request.FileSize)
                .GreaterThan(0);

            RuleFor(x => x.Request.FileData)
                .NotNull()
                .Must(file => file is { Length: > 0 })
                .WithMessage("Uploaded document is required.");
        });
    }
}
