
using TodoHelper.Domain.Errors;

namespace TodoHelper.Domain.Results;

public sealed class Result<T>
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public T? Payload { get; }
    public Error Error { get; }

    private Result(bool isSuccess, T? payload, Error error)
    {
        IsSuccess = isSuccess;
        Payload = payload;
        Error = error;
    }

    public static Result<T> Success(T payload) => new(true, payload, Error.None);
    public static Result<T> Failure(Error error) => new(false, default, error);
}
