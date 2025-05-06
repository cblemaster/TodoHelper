
using Microsoft.AspNetCore.Http.HttpResults;
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Application.Features.Common.Specifications;
using TodoHelper.Application.Interfaces;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.GetTodosOverdue;

internal static class GetTodosOverdueEndpoint
{
    internal static WebApplication MapGetTodosOverdueEndpoint(this WebApplication app)
    {
        app.MapGet(
            pattern: "/todo/overdue",
            handler: async Task<Results<Ok<ICollection<TodoDTO>>, InternalServerError<string>>>
                (ICommandHandler<GetTodosOverdueCommand, GetTodosOverdueResponse> handler) =>
                {
                    GetTodosOverdueCommand command = new();
                    Result<GetTodosOverdueResponse> response = await handler.HandleAsync(command);
                    return response.IsSuccess && response.Value is not null && response.Value.OverdueTodos is ICollection<TodoDTO> todos
                        ? TypedResults.Ok(todos)
                        : TypedResults.InternalServerError(ApplicationErrors.UnknownErrorMessage("getting todos"));
                }
            );
        return app;
    }
}
