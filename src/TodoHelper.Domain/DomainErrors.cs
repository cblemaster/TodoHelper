
namespace TodoHelper.Domain;

public sealed class DomainErrors
{
    public static string IsNotUniqueErrorMessage(string entityName, string fieldName, string value) =>
        $"{entityName} with {fieldName} {value} already exixts.";
    public static string CannotDeleteImportantTodosErrorMessage() => "Important todos cannot be deleted.";
    public static string CannotUpdateCompletedTodosErrorMessage() => "Completed todos cannot be updated.";
    public static string TodoCategoryIsNullErrorMessage() => "Todos must belong to a category.";
    public static string UnknownErrorMessage(string action) =>
        $"An unknown error occurred when {action}.";
}
