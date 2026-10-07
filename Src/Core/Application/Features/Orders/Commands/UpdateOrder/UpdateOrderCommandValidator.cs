using FluentValidation;
using SwaOlova.Domain.Enums;

namespace SwaOlova.Application.Features.Orders.Commands.UpdateOrder;

public sealed class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x => x.OrderId).NotEmpty();
        RuleFor(x => x.Request).NotNull();

        When(x => x.Request is not null, () =>
        {
            RuleFor(x => x.Request.DeliveryAddressId).NotEmpty();
            RuleFor(x => x.Request.OrderType)
                .IsInEnum()
                .NotEqual(OrderType.None);
            RuleFor(x => x.Request.DeliveryFee)
                .GreaterThanOrEqualTo(0);
        });
    }
}
