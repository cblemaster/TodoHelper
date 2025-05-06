
using TodoHelper.Domain.Extensions;
using TodoHelper.Domain.Results;

namespace TodoHelper.Domain.ValueObjects;

public sealed class Descriptor
{
    public string Value { get; }

    private Descriptor(string value) => Value = value;

    public static Result<Descriptor> Create(string value, string attributeName, int maxLength)
    {
        (bool IsValid, string Error) = value.ValidateDescriptor(attributeName, maxLength);
        return IsValid
            ? Result<Descriptor>.Success(new Descriptor(value))
            : Result<Descriptor>.ValidationFailure(Error);
    }

    internal static string MaxLengthExceededErrorMessage(string attributeName, int maxLength) =>
        $"{attributeName} must be {maxLength} characters or fewer.";
    internal static string IsNullEmptyOrWhitespaceErrorMessage(string attributeName) =>
        $"{attributeName} is required and cannot consist of exclusively whitespace characters.";
}
