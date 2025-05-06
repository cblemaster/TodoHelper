
using TodoHelper.Domain.Results;

namespace TodoHelper.Domain.ValueObjects;

public sealed class Name
{
    public const int MAX_LENGTH = 40;

    public string Value { get; }

    private Name(string value) => Value = value;

    public static Result<Name> Create(string value)
    {
        (bool IsValid, string error) = Validate(value);
        return !IsValid
            ? Result<Name>.ValidationFailure(error)
            : Result<Name>.Success(new Name(value));

        static (bool IsValid, string error) Validate(string value) =>
            string.IsNullOrWhiteSpace(value)
                ? (false, DomainValidationErrors.IsNullEmptyOrWhitespaceErrorMessage(nameof(Name)))
                : value.Length > Name.MAX_LENGTH
                    ? (false, DomainValidationErrors.MaxLengthExceededErrorMessage(nameof(Name), Name.MAX_LENGTH))
                    : (true, string.Empty);
    }
}
