
using Microsoft.AspNetCore.Http.HttpResults;
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Application.Features.Common.Specifications;
using TodoHelper.Application.Interfaces;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.GetTodosImportant;

internal static class GetTodosImportantEndpoint
{
    internal static WebApplication MapGetTodosImportantEndpoint(this WebApplication app)
    {
        app.MapGet(
            pattern: "/todo/important",
            handler: async Task<Results<Ok<ICollection<TodoDTO>>, InternalServerError<string>>>
                (ICommandHandler<GetTodosImportantCommand, GetTodosImportantResponse> handler) =>
                {
                    GetTodosImportantCommand command = new();
                    Result<GetTodosImportantResponse> response = await handler.HandleAsync(command);
                    return response.IsSuccess && response.Value is not null && response.Value.ImportantTodos is ICollection<TodoDTO> todos
                        ? TypedResults.Ok(todos)
                        : TypedResults.InternalServerError(ApplicationErrors.UnknownErrorMessage("getting todos"));
                }
            );
        return app;
    }
}
