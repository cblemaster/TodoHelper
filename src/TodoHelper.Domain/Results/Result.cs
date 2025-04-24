
namespace TodoHelper.Domain.Results;

internal class Result<T>
{
    internal bool IsSuccess { get; }
    internal bool IsFailure => !IsSuccess;
    internal T? Value { get; }
    internal string? Error { get; }

    private Result(bool isSucess, T? value, string? error)
    {
        IsSuccess = isSucess;
        Value = value;
        Error = error;
    }

    internal static Result<T> Success(T value) => new(true, value, null);

    internal static Result<T> Failure(string error) => new(false, default, error);
}
