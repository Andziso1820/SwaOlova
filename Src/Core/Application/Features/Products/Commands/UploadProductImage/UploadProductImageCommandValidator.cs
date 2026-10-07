using FluentValidation;

namespace SwaOlova.Application.Features.Products.Commands.UploadProductImage;

public sealed class UploadProductImageCommandValidator : AbstractValidator<UploadProductImageCommand>
{
    public UploadProductImageCommandValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();

        RuleFor(x => x.Request).NotNull();

        When(x => x.Request is not null, () =>
        {
            RuleFor(x => x.Request.ImageUrl)
                .NotEmpty()
                .MaximumLength(500)
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
                .WithMessage("ImageUrl must be a valid URL.");
        });
    }
}
