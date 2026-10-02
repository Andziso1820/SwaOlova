using FluentValidation;

namespace SwaOlova.Application.Features.Products.Commands.UpdateInventory;

public sealed class UpdateInventoryCommandValidator : AbstractValidator<UpdateInventoryCommand>
{
    public UpdateInventoryCommandValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();

        RuleFor(x => x.Request).NotNull();

        When(x => x.Request is not null, () =>
        {
            RuleFor(x => x.Request.QuantityAvailable)
                .GreaterThanOrEqualTo(0);
        });
    }
}
