using System.Reflection;
using SwaOlova.Application.Common.Models;

namespace SwaOlova.Application.Common.Behaviors;

internal static class ResultResponseFactory
{
    public static TResponse CreateFailure<TResponse>(params string[] errors)
        => CreateFailure<TResponse>(errors.AsEnumerable());

    public static TResponse CreateFailure<TResponse>(Exception exception, params string[] errors)
    {
        var response = CreateFailure<TResponse>(errors.AsEnumerable());

        if (response is Result result)
        {
            result.WithException(exception);
        }

        return response;
    }

    public static TResponse CreateFailure<TResponse>(IEnumerable<string> errors)
    {
        var responseType = typeof(TResponse);
        var errorArray = errors.Where(x => !string.IsNullOrWhiteSpace(x)).Distinct().ToArray();

        if (responseType == typeof(Result))
        {
            return (TResponse)(object)Result.Failure(errorArray);
        }

        if (responseType == typeof(ValidationResult))
        {
            return (TResponse)(object)ValidationResult.Create(errorArray);
        }

        if (responseType.IsGenericType && responseType.GetGenericTypeDefinition() == typeof(Result<>))
        {
            var failureMethod = responseType.GetMethod(
                nameof(Result<object>.Failure),
                BindingFlags.Public | BindingFlags.Static,
                [typeof(IEnumerable<string>)]);

            if (failureMethod is not null)
            {
                return (TResponse)failureMethod.Invoke(null, [errorArray])!;
            }
        }

        throw new InvalidOperationException($"Unable to create a failure response for {responseType.Name}.");
    }
}
