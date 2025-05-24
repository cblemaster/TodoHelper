
using Microsoft.AspNetCore.Http.HttpResults;
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Errors;
using _Todo = TodoHelper.Domain.Entities.Todo;
using GetTodos = TodoHelper.Application.Features.Todo.GetAllImportant;

namespace TodoHelper.Application.Features.Todo.GetAllImportant;

internal static class EndpointExtension
{
    internal static WebApplication MapGetAllTodoImportant(this WebApplication app)
    {
        _ = app.MapGet
            (
                pattern: "/todo",
                handler: async Task<Results<InternalServerError<string>, Ok<IEnumerable<TodoDTO>>>>
                (IRepository<_Todo> repository, GetTodos.Handler handler) =>
                {
                    GetTodos.Command command = new(true);   // TODO: !!!
                    GetTodos.Response response = await handler.HandleAsync(command);
                    return response.Todos.IsFailure &&
                        response.Todos.Error is not null
                            ? TypedResults.InternalServerError(Error.Unknown.Description)
                            : response.Todos.IsSuccess &&
                                response.Todos.Payload is IEnumerable<TodoDTO> dtos
                                    ? TypedResults.Ok(dtos)
                                    : TypedResults.InternalServerError(Error.Unknown.Description);
                }
            );
        return app;
    }
}
