
using TodoHelper.Domain.Results;

namespace TodoHelper.Domain.ValueObjects;

public sealed class Description
{
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
                ? (false, DomainErrors.IsNullEmptyOrWhitespaceErrorMessage(nameof(Description)))
                : value.Length > DomainErrors.TODO_DESCRIPTION_MAX_LENGTH
                    ? (false, DomainErrors.MaxLengthExceededErrorMessage(nameof(Description), DomainErrors.TODO_DESCRIPTION_MAX_LENGTH))
                    : (true, string.Empty);
    }
}
