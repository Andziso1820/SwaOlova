namespace SwaOlova.Application.Common.Models;

public sealed class Result<T> : Result
{
    private Result(bool succeeded, T? value, IEnumerable<string>? errors = null)
        : base(succeeded, errors)
    {
        Value = value;
    }

    public T? Value { get; }

    public static Result<T> Success(T value) => new(true, value);

    public static new Result<T> Failure(params string[] errors) => new(false, default, errors);

    public static Result<T> Failure(IEnumerable<string> errors) => new(false, default, errors);
}