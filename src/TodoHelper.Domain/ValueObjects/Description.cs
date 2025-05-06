
using TodoHelper.Domain.Results;

namespace TodoHelper.Domain.ValueObjects;

public sealed class Description
{
    public const int MAX_LENGTH = 255;

    public string Value { get; }

    private Description(string value) => Value = value;

    public static Result<Description> Create(string value)
    {
        (bool IsValid, string error) = Validate(value);
        return !IsValid
            ? Result<Description>.ValidationFailure(error)
            : Result<Description>.Success(new Description(value));

        // TODO: this is the exact same validation as in Name.cs...
        static (bool IsValid, string error) Validate(string value) =>
            string.IsNullOrWhiteSpace(value)
                ? (false, DomainValidationErrors.IsNullEmptyOrWhitespaceErrorMessage(nameof(Description)))
                : value.Length > Description.MAX_LENGTH
                    ? (false, DomainValidationErrors.MaxLengthExceededErrorMessage(nameof(Description), Description.MAX_LENGTH))
                    : (true, string.Empty);
    }
}
