
using TodoHelper.Domain.Results;

namespace TodoHelper.Domain.ValueObjects;

public sealed class Name
{
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
            ? (false, DomainErrors.IsNullEmptyOrWhitespaceErrorMessage(nameof(Name)))
            : value.Length > DomainErrors.CATEGORY_NAME_MAX_LENGTH
                ? (false, DomainErrors.MaxLengthExceededErrorMessage(nameof(Name), DomainErrors.CATEGORY_NAME_MAX_LENGTH))
                : (true, string.Empty);
    }
}
