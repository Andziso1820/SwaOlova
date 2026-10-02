using FluentValidation;

namespace SwaOlova.Application.Features.Riders.Queries.GetRiderById;

public sealed class GetRiderByIdQueryValidator : AbstractValidator<GetRiderByIdQuery>
{
    public GetRiderByIdQueryValidator()
    {
        RuleFor(x => x.RiderId).NotEmpty();
    }
}
