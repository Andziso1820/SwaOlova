using MediatR;
using Microsoft.Extensions.Logging;
using SwaOlova.Application.Common.Exceptions;

namespace SwaOlova.Application.Common.Behaviors;

public sealed class ExceptionHandlingBehavior<TRequest, TResponse>(ILogger<ExceptionHandlingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (ValidationException exception)
        {
            logger.LogWarning(exception, "Validation failure for request {RequestName}", typeof(TRequest).Name);
            return ResultResponseFactory.CreateFailure<TResponse>(exception.Errors);
        }
        catch (NotFoundException exception)
        {
            logger.LogWarning(exception, "Entity not found while handling request {RequestName}", typeof(TRequest).Name);
            return ResultResponseFactory.CreateFailure<TResponse>(exception.Message);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Unhandled exception for request {RequestName}", typeof(TRequest).Name);
            return ResultResponseFactory.CreateFailure<TResponse>("An unexpected application error occurred.");
        }
    }
}
