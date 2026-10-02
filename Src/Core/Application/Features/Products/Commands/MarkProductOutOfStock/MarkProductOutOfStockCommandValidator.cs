using FluentValidation;

namespace SwaOlova.Application.Features.Products.Commands.MarkProductOutOfStock;

public sealed class MarkProductOutOfStockCommandValidator : AbstractValidator<MarkProductOutOfStockCommand>
{
    public MarkProductOutOfStockCommandValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
    }
}
