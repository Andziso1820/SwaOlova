using FluentValidation;

namespace SwaOlova.Application.Features.Products.Commands.UpdateProduct;

public sealed class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();

        RuleFor(x => x.Request).NotNull();

        When(x => x.Request is not null, () =>
        {
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
