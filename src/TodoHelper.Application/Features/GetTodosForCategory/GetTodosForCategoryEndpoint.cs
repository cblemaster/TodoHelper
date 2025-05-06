
using Microsoft.AspNetCore.Http.HttpResults;
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Application.Features.Common.Specifications;
using TodoHelper.Application.Interfaces;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.GetTodosForCategory;

internal static class GetTodosForCategoryEndpoint
{
    internal static RouteHandlerBuilder MapGetTodosForCategoryEndpoint(this WebApplication app) => app.MapGet(pattern: "/category/{id:guid}/todo",
    handler: async Task<Results<Ok<ICollection<TodoDTO>>, InternalServerError<string>>>
    (Guid id, ICommandHandler<GetTodosForCategoryCommand, GetTodosForCategoryResponse> handler) =>
    {
        GetTodosForCategoryCommand command = new(id);
        Result<GetTodosForCategoryResponse> response = await handler.HandleAsync(command);
        return response.IsSuccess && response.Value is not null && response.Value.TodosForCategory is ICollection<TodoDTO> todos
            ? TypedResults.Ok(todos)
            : TypedResults.InternalServerError(ApplicationErrors.UnknownErrorMessage("getting todos"));
    });
}
