
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

    internal static Result<T> Success(T value)
    {
        return new Result<T>(true, value, null);
    }

    internal static Result<T> Failure(string error)
    {
        return new Result<T>(false, default, error);
    }
}
