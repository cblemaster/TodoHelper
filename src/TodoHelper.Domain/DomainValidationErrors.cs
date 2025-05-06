
namespace TodoHelper.Domain;

internal class DomainValidationErrors
{
    //public const int CATEGORY_NAME_MAX_LENGTH = 40;
    //public const int TODO_DESCRIPTION_MAX_LENGTH = 255;

    public static string MaxLengthExceededErrorMessage(string fieldName, int maxLength) =>
        $"{fieldName} must be {maxLength} characters or fewer.";
    public static string IsNullEmptyOrWhitespaceErrorMessage(string fieldName) =>
        $"{fieldName} is required and cannot consist of exclusively whitespace characters.";
}
