using FluentValidation;

namespace SwaOlova.Application.Features.Orders.Commands.CompleteOrder;

public sealed class CompleteOrderCommandValidator : AbstractValidator<CompleteOrderCommand>
{
    public CompleteOrderCommandValidator()
    {
        RuleFor(x => x.OrderId).NotEmpty();
    }
}
