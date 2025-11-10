using System.Diagnostics.CodeAnalysis;

namespace SharedLibrary.Result;

// This needs to be fleshed out more...
public sealed record Error(string Code, string? Description = null, string? StackTrace = null)
{
    public static readonly Error None = new(string.Empty);

    public static readonly Error NullValue = new("General.Null", "Null value was provided");

    public static implicit operator Result(Error error) => Result.Failure(error);
}

public class Result
{
    public Result(bool isSuccess,
                  Error error)
    {
        if (isSuccess && error != Error.None ||
            !isSuccess && error == Error.None)
        {
            throw new ArgumentException("Invalid error", nameof(error));
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }

    public Error Error { get; }

    public static Result Success() => new(true, Error.None);

    public static Result<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);

    public static Result Failure(Error error) => new(false, error);

    public static Result<TValue> Failure<TValue>(Error error) => new(default, false, error);
}

public class Result<TValue>(TValue? value, bool isSuccess, Error error) : Result(isSuccess, error)
{
    [NotNull]
    public TValue Value => IsSuccess ? 
                            value! : 
                            throw new InvalidOperationException("The value of a failure result can't be accessed.");

    public static implicit operator Result<TValue>(TValue? value) => value is not null ? 
                                                                      Success(value) : 
                                                                      Failure<TValue>(Error.NullValue);

    public static Result<TValue> ValidationFailure(Error error) => new(default, false, error);
}