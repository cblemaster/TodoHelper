
using TodoHelper.Domain.Entities;

namespace TodoHelper.Domain.Rules;

internal static class StringExtensions
{
    internal const int DESCRIPTION_MAX_LENGTH = 255;
    internal const int NAME_MAX_LENGTH = 40;

    internal static string ReturnTodoWithValidDescriptionValueOrThrow(this string descriptionValue) =>
        string.IsNullOrWhiteSpace(descriptionValue) || descriptionValue.Length > DESCRIPTION_MAX_LENGTH
                ? throw new ArgumentException($"{nameof(descriptionValue)} is required, cannot be all whitespace characters, and must be {DESCRIPTION_MAX_LENGTH} or fewer characters.", nameof(descriptionValue))
                : descriptionValue;
    internal static string ReturnCategoryWithValidNameValueOrThrow(this string nameValue) =>
        string.IsNullOrWhiteSpace(nameValue) || nameValue.Length > NAME_MAX_LENGTH
            ? throw new ArgumentException($"{nameof(nameValue)} is required, cannot be all whitespace characters, and must be {NAME_MAX_LENGTH} or fewer characters.", nameof(nameValue))
            : nameValue;
}
