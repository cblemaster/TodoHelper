
namespace TodoHelper.Domain.Errors;

internal static class CategoryErrors
{
    public static Error NotFound(Guid id) =>
        new("Categories.NotFound", $"The category with Id '{id}' was not found.");

    public static Error NameValueNotValid() =>
        new("Categories.NameValueNotValid", "Category name is required and cannot consist of exclusively whitespace charaters.");

    public static Error NameLengthNotValid(int maxLength) =>
        new("Categories.NameLengthNotValid", $"Category name cannot exceed {maxLength} characters.");
}
