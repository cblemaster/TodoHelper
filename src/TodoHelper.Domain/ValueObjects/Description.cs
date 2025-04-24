
using TodoHelper.Domain.Results;

namespace TodoHelper.Domain.ValueObjects;

internal sealed class Description
{
    private const int MAX_LENGTH = 255;

    internal string Value { get; }

    private Description(string value) => Value = value;

    internal static Result<Description> CreateNew(string value)
    {
        (bool IsValid, string error) = Validate(value);
        return !IsValid ? Result<Description>.Failure(error) : Result<Description>.Success(new Description(value));
    }

    private static (bool IsValid, string error) Validate(string value) =>
        string.IsNullOrWhiteSpace(value)
            ? (false, $"{nameof(value)} is required, and cannot be all whitespace characters.")
            : value.Length > MAX_LENGTH
                ? (false, $"{nameof(value)} must be {MAX_LENGTH} or fewer characters.")
                : (true, string.Empty);
}
