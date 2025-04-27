
using TodoHelper.Domain.Results;

namespace TodoHelper.Domain.ValueObjects;

public sealed class Name
{
    private const int MAX_LENGTH = 40;

    public string Value { get; }

    private Name(string value) => Value = value;

    public static Result<Name> Create(string value)
    {
        (bool IsValid, string error) = Validate(value);
        return !IsValid ? Result<Name>.Failure(error) : Result<Name>.Success(new Name(value));
    }

    private static (bool IsValid, string error) Validate(string value) =>
        string.IsNullOrWhiteSpace(value)
            ? (false, $"{nameof(value)} is required, and cannot be all whitespace characters.")
            : value.Length > MAX_LENGTH
                ? (false, $"{nameof(value)} must be {MAX_LENGTH} or fewer characters.")
                : (true, string.Empty);
}
