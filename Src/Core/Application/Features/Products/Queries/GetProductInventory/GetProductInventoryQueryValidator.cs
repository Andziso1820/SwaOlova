using FluentValidation;

namespace SwaOlova.Application.Features.Products.Queries.GetProductInventory;

public sealed class GetProductInventoryQueryValidator : AbstractValidator<GetProductInventoryQuery>
{
    public GetProductInventoryQueryValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
    }
}
