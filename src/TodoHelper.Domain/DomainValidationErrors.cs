
namespace TodoHelper.Domain;

internal class DomainValidationErrors
{
    public static string MaxLengthExceededErrorMessage(string fieldName, int maxLength) =>
        $"{fieldName} must be {maxLength} characters or fewer.";
    public static string IsNullEmptyOrWhitespaceErrorMessage(string fieldName) =>
        $"{fieldName} is required and cannot consist of exclusively whitespace characters.";
}
