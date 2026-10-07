using FluentValidation;

namespace SwaOlova.Application.Features.Products.Commands.CreateProduct;

public sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Request).NotNull();

        When(x => x.Request is not null, () =>
        {
            RuleFor(x => x.Request.MerchantId).NotEmpty();

            RuleFor(x => x.Request.CategoryId).NotEmpty();

            RuleFor(x => x.Request.Name)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Request.Description)
                .NotEmpty()
                .MaximumLength(1000);

            RuleFor(x => x.Request.Price)
                .GreaterThan(0m);

            RuleFor(x => x.Request.PreparationTimeMinutes)
                .GreaterThanOrEqualTo(0);
        });
    }
}
