using FluentValidation;

namespace SwaOlova.Application.Features.Riders.Commands.UpdateLocation;

public sealed class UpdateLocationCommandValidator : AbstractValidator<UpdateLocationCommand>
{
    public UpdateLocationCommandValidator()
    {
        RuleFor(x => x.RiderId).NotEmpty();
        RuleFor(x => x.Request).NotNull();

        When(x => x.Request is not null, () =>
        {
            RuleFor(x => x.Request.Latitude)
                .InclusiveBetween(-90m, 90m);
            RuleFor(x => x.Request.Longitude)
                .InclusiveBetween(-180m, 180m);
        });
    }
}
