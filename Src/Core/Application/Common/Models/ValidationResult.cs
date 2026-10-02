namespace SwaOlova.Application.Common.Models;

public sealed class ValidationResult : Result
{
    private ValidationResult(IEnumerable<string> errors)
        : base(false, errors)
    {
    }

    public static ValidationResult Create(IEnumerable<string> errors) => new(errors);
}