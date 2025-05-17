
using Microsoft.AspNetCore.Http.HttpResults;
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Errors;
using TodoHelper.Domain.Results;
using _Todo = TodoHelper.Domain.Entities.Todo;
using GetTodos = TodoHelper.Application.Features.Todo.GetAll;

namespace TodoHelper.Application.Features.Todo.GetAll;

internal static class EndpointExtension
{
    internal static WebApplication MapGetAllTodo(this WebApplication app)
    {
        _ = app.MapGet
            (
                pattern: "/todo",
                handler: async Task<Results<InternalServerError<string>, Ok<IEnumerable<TodoDTO>>>>
                (IRepository<_Todo> repository, GetTodos.Handler handler) =>
                {
                    GetTodos.Command command = new();
                    Result<GetTodos.Response> result = await handler.HandleAsync(command);
                    return result.IsFailure && result.Error is not null
                        ? TypedResults.InternalServerError(Error.Unknown.Description)
                        : result.IsSuccess && result.Value is GetTodos.Response response
                            ? TypedResults.Ok(response.Todos)
                            : TypedResults.InternalServerError(Error.Unknown.Description);
                }
            );
        return app;
    }
}
