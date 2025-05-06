
using TodoHelper.Domain.ValueObjects;

namespace TodoHelper.Domain.Extensions;

internal static class StringExtensions
{
    internal static (bool IsValid, string ValidationError) ValidateDescriptor(this string s, string attributeName, int maxLength) =>
        string.IsNullOrWhiteSpace(s)
            ? (false, Descriptor.IsNullEmptyOrWhitespaceErrorMessage(attributeName))
            : s.Length > maxLength
                ? (false, Descriptor.MaxLengthExceededErrorMessage(attributeName, maxLength))
                : (true, string.Empty);
}
