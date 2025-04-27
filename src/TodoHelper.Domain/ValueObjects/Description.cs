
using TodoHelper.Domain.Results;

namespace TodoHelper.Domain.ValueObjects;

public sealed class Description
{
    private const int MAX_LENGTH = 255;

    public string Value { get; }

    private Description(string value) => Value = value;

    internal static Result<Description> CreateNew(string value)
    {
        (bool IsValid, string error) = Validate(value);
        return !IsValid ? Result<Description>.Failure(error) : Result<Description>.Success(new Description(value));
    }

    // TODO: this is the exact same validation as in Name.cs...
    private static (bool IsValid, string error) Validate(string value) =>
        string.IsNullOrWhiteSpace(value)
            ? (false, $"{nameof(value)} is required, and cannot be all whitespace characters.")
            : value.Length > MAX_LENGTH
                ? (false, $"{nameof(value)} must be {MAX_LENGTH} or fewer characters.")
                : (true, string.Empty);
}
