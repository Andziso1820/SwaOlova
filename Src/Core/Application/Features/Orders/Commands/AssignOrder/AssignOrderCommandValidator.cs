using FluentValidation;

namespace SwaOlova.Application.Features.Orders.Commands.AssignOrder;

public sealed class AssignOrderCommandValidator : AbstractValidator<AssignOrderCommand>
{
    public AssignOrderCommandValidator()
    {
        RuleFor(x => x.OrderId).NotEmpty();
        RuleFor(x => x.Request).NotNull();

        When(x => x.Request is not null, () =>
        {
            RuleFor(x => x.Request.RiderId).NotEmpty();
        });
    }
}
