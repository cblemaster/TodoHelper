
using Microsoft.AspNetCore.Http.HttpResults;
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Errors;
using TodoHelper.Domain.Results;
using _Todo = TodoHelper.Domain.Entities.Todo;
using CreateTodo = TodoHelper.Application.Features.Todo.Create;

namespace TodoHelper.Application.Features.Todo.Create;

internal static class EndpointExtension
{
    internal static WebApplication MapCreateTodo(this WebApplication app)
    {
        _ = app.MapPost
            (
                pattern: "/todo",
                handler: async Task<Results<BadRequest<string>, Created<TodoDTO>, InternalServerError<string>>>
                (IRepository<_Todo> repository, CreateTodo.Command command, CreateTodo.Handler handler) =>
                {
                    Response response = await handler.HandleAsync(command);
                    return response.Todo.IsFailure && response.Todo.Error is Error error && error.ErrorCode == ErrorCode.NotValid
                        ? TypedResults.BadRequest(error.Description)
                        : response.Todo.IsSuccess && response.Todo.Payload is TodoDTO dto
                            ? TypedResults.Created("no uri for this resource", dto)
                            : TypedResults.InternalServerError(Error.Unknown.Description);
                }
            );
        return app;
    }
}
