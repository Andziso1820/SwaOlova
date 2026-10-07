using FluentValidation;

namespace SwaOlova.Application.Features.Riders.Commands.UploadDocument;

public sealed class UploadDocumentCommandValidator : AbstractValidator<UploadDocumentCommand>
{
    public UploadDocumentCommandValidator()
    {
        RuleFor(x => x.RiderId).NotEmpty();
        RuleFor(x => x.Request).NotNull();

        When(x => x.Request is not null, () =>
        {
            RuleFor(x => x.Request.FileName)
                .NotEmpty()
                .MaximumLength(255);
            RuleFor(x => x.Request.FileUrl)
                .NotEmpty()
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
                .WithMessage("FileUrl must be a valid URL.");
        });
    }
}
