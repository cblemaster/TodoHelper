
using Microsoft.AspNetCore.Http.HttpResults;
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Errors;
using _Todo = TodoHelper.Domain.Entities.Todo;
using GetTodo = TodoHelper.Application.Features.Todo.Get;

namespace TodoHelper.Application.Features.Todo.Get;

internal static class EndpointExtension
{
    internal static WebApplication MapGetTodo(this WebApplication app)
    {
        _ = app.MapGet
            (
                pattern: "/todo/{id:guid}",
                handler: async Task<Results<InternalServerError<string>, NotFound<string>,
                    Ok<TodoDTO>>>
                    (IRepository<_Todo> repository, GetTodo.Handler handler, Guid id) =>
                    {
                        GetTodo.Command command = new(id);
                        GetTodo.Response response = await handler.HandleAsync(command);
                        return response.Todo.IsFailure &&
                            response.Todo.Error is Error error &&
                            error.ErrorCode == ErrorCode.NotFound
                                ? TypedResults.NotFound(error.Description)
                                : response.Todo.IsSuccess &&
                                    response.Todo.Payload is TodoDTO dto
                                        ? TypedResults.Ok(dto)
                                        : TypedResults.InternalServerError(Error.Unknown.Description);
                    }
            );
        return app;
    }
}
