
namespace TodoHelper.Domain.Primitives.Extensions;

internal static class StringExtensions
{
    // TODO: replace string validation with pattern matching
    internal static bool IsValueValid(this string s) => !string.IsNullOrWhiteSpace(s);
    internal static bool IsLengthValid(this string s, uint maxLength) =>
        s.Length >= 1 && s.Length <= maxLength;
}
