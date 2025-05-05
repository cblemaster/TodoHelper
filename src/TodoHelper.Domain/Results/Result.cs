
namespace TodoHelper.Domain.Results;

public sealed class Result<T>
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public T? Value { get; }
    public string? Error { get; }

    private Result(bool isSucess, T? value, string? error)
    {
        IsSuccess = isSucess;
        Value = value;
        Error = error;
    }

    public static Result<T> Success(T value) => new(true, value, null);
    public static Result<T> ValidationFailure(string error) => new(false, default, error);
    public static Result<T> DomainRuleFailure(string error) => new(false, default, error);
    public static Result<T> NotFoundFailure(string error) => new(false, default, error);
    public static Result<T> UnknownFailure(string error) => new(false, default, error);
}
