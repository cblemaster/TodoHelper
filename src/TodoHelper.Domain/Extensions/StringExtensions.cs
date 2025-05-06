
namespace TodoHelper.Domain.Extensions;

internal static class StringExtensions
{
    internal static (bool IsValid, string ValidationError) ValidateAttribute(this string s, string attributeName, int maxLength)
    {
        if (string.IsNullOrWhiteSpace(s))
        {
            return (false, DomainValidationErrors.IsNullEmptyOrWhitespaceErrorMessage(attributeName));
        }
        if (s.Length > maxLength)
        {
            return (false, DomainValidationErrors.MaxLengthExceededErrorMessage(attributeName, maxLength));
        }
        return (true, string.Empty);
    }
}
