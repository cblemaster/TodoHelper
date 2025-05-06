
using Microsoft.AspNetCore.Http.HttpResults;
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Application.Features.Common.Specifications;
using TodoHelper.Application.Interfaces;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.GetTodosCompleted;

internal static class GetTodosCompletedEndpoint
{
    internal static RouteHandlerBuilder MapGetTodosCompletedEndpoint(this WebApplication app) => app.MapGet(pattern: "/todo/complete",
    handler: async Task<Results<Ok<ICollection<TodoDTO>>, InternalServerError<string>>>
    (ICommandHandler<GetTodosCompletedCommand, GetTodosCompletedResponse> handler) =>
    {
        GetTodosCompletedCommand command = new();
        Result<GetTodosCompletedResponse> response = await handler.HandleAsync(command);
        return response.IsSuccess && response.Value is not null && response.Value.CompleteTodos is ICollection<TodoDTO> todos
            ? TypedResults.Ok(todos)
            : TypedResults.InternalServerError(ApplicationErrors.UnknownErrorMessage("getting todos"));
    });
}
