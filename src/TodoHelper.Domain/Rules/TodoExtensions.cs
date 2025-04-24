
using TodoHelper.Domain.Entities;

namespace TodoHelper.Domain.Rules;

internal static class TodoExtensions
{
    internal const int DESCRIPTION_MAX_LENGTH = 255;

    internal static string ReturnWithValidDescriptionOrThrow(this string s) =>
        string.IsNullOrWhiteSpace(s) || s.Length > DESCRIPTION_MAX_LENGTH
                ? throw new ArgumentException($"{nameof(Todo.Description)} is required, cannot be all whitespace characters, and must be {DESCRIPTION_MAX_LENGTH} or fewer characters.", nameof(Todo.Description))
                : s;

    internal static bool CanBeUpdated(this Todo todo) => !todo.IsComplete;

    internal static bool CanBeDeleted(this Todo todo) => !todo.Importance.IsImportant;
}
