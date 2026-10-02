namespace SwaOlova.Application.Common.Exceptions;

public class ValidationException : Exception
{
    public ValidationException(IEnumerable<string> errors)
        : base("One or more validation failures occurred.")
    {
        Errors = errors.ToArray();
    }

    public IReadOnlyCollection<string> Errors { get; }
}