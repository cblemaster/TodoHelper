
using TodoHelper.Domain.Entities;

namespace TodoHelper.Domain.Rules;

internal static class CategoryExtensions
{
    internal const int NAME_MAX_LENGTH = 40;

    internal static string ReturnWithValidNameOrThrow(this string s) =>
        string.IsNullOrWhiteSpace(s) || s.Length > NAME_MAX_LENGTH
            ? throw new ArgumentException($"{nameof(Category.Name)} is required, cannot be all whitespace characters, and must be {NAME_MAX_LENGTH} or fewer characters.", nameof(Category.Name))
            : s;

    internal static bool CanBeDeleted(this Category category) => !category.Todos.Any();
}
