
namespace TodoHelper.Domain.Errors;

internal static class TodoErrors
{
    public static Error NotFound(Guid id) =>
        new("Todos.NotFound", $"The todo with Id '{id}' was not found.");

    public static Error DescriptionValueNotValid() =>
        new("Todos.DescriptionValueNotValid", "Todo description is required and cannot consist of exclusively whitespace charaters.");

    public static Error DescriptionLengthNotValid(int maxLength) =>
        new("Todos.DescriptionLengthNotValid", $"Todo description cannot exceed {maxLength} characters.");
}
