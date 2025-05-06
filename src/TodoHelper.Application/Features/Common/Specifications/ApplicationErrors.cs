namespace TodoHelper.Application.Features.Common.Specifications;

internal sealed class ApplicationErrors
{
    internal static string NotFoundErrorMessage(string entityName, Guid id) => $"{entityName} with id {id} not found.";
    internal static string UnknownErrorMessage(string action) => $"An unknown error occurred when {action}.";
}
