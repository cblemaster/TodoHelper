
namespace TodoHelper.Domain.Extensions;

internal static class StringExtensions
{
    internal static (bool IsValid, string ValidationError) ValidateAttribute(this string s, string attributeName, int maxLength) =>
        string.IsNullOrWhiteSpace(s)
            ? (false, DomainValidationErrors.IsNullEmptyOrWhitespaceErrorMessage(attributeName))
            : s.Length > maxLength
                ? (false, DomainValidationErrors.MaxLengthExceededErrorMessage(attributeName, maxLength))
                : (true, string.Empty);
}
