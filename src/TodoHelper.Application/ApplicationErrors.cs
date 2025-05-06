
namespace TodoHelper.Application;

public sealed class ApplicationErrors
{
    public static string NotFoundErrorMessage(string entityName, Guid id) =>
        $"{entityName} with id {id} not found.";
    public static string UnknownErrorMessage(string action) =>
        $"An unknown error occurred when {action}.";
}
