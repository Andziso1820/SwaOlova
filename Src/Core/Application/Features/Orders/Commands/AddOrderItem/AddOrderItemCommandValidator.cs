using FluentValidation;

namespace SwaOlova.Application.Features.Orders.Commands.AddOrderItem;

public sealed class AddOrderItemCommandValidator : AbstractValidator<AddOrderItemCommand>
{
    public AddOrderItemCommandValidator()
    {
        RuleFor(x => x.OrderId).NotEmpty();
        RuleFor(x => x.Request).NotNull();

        When(x => x.Request is not null, () =>
        {
            RuleFor(x => x.Request.ProductId).NotEmpty();
            RuleFor(x => x.Request.Quantity).GreaterThan(0);
        });
    }
}
