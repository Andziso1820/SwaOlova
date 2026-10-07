using FluentValidation;
using MediatR;
using SwaOlova.Application.Common.Models;

namespace SwaOlova.Application.Common.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);
        var validationResults = await Task.WhenAll(validators.Select(x => x.ValidateAsync(context, cancellationToken)));
        var errors = validationResults
            .SelectMany(x => x.Errors)
            .Where(x => x is not null)
            .Select(x => x.ErrorMessage)
            .Distinct()
            .ToArray();

        if (errors.Length == 0)
        {
            return await next();
        }

        return ResultResponseFactory.CreateFailure<TResponse>(errors);
    }
}
