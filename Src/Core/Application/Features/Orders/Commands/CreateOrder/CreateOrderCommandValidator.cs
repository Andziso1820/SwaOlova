using FluentValidation;
using SwaOlova.Domain.Enums;

namespace SwaOlova.Application.Features.Orders.Commands.CreateOrder;

public sealed class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.Request).NotNull();

        When(x => x.Request is not null, () =>
        {
            RuleFor(x => x.Request.CustomerId).NotEmpty();
            RuleFor(x => x.Request.MerchantId).NotEmpty();
            RuleFor(x => x.Request.DeliveryAddressId).NotEmpty();
            RuleFor(x => x.Request.OrderType)
                .IsInEnum()
                .NotEqual(OrderType.None);
            RuleFor(x => x.Request.DeliveryFee)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Request.Items)
                .NotNull()
                .Must(items => items.Count > 0)
                .WithMessage("At least one order item is required.");

            RuleForEach(x => x.Request.Items).ChildRules(item =>
            {
                item.RuleFor(x => x.ProductId).NotEmpty();
                item.RuleFor(x => x.Quantity).GreaterThan(0);
            });
        });
    }
}
