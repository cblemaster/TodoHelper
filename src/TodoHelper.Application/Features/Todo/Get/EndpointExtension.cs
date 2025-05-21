
using Microsoft.AspNetCore.Http.HttpResults;
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Errors;
using TodoHelper.Domain.Results;
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
                        Result<GetTodo.Response> result = await handler.HandleAsync(command);
                        return result.IsFailure
                            && result.Error is Error error
                            && error.ErrorCode == ErrorCode.NotFound
                            ? TypedResults.NotFound(error.Description)
                            : result.IsSuccess
                                && result.Payload is not null and GetTodo.Response response
                                ? TypedResults.Ok(response.Todo)
                                : TypedResults.InternalServerError(Error.Unknown.Description);
                    }
            );
        return app;
    }
}
