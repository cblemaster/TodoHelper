
namespace TodoHelper.Domain.Primitives.Extensions;

public static class StringExtensions
{
    public static bool IsValueValid(this string s) => !string.IsNullOrWhiteSpace(s);
    public static bool IsLengthValid(this string s, uint maxLength) =>
        s.Length >= 1 && s.Length <= maxLength;  
}
