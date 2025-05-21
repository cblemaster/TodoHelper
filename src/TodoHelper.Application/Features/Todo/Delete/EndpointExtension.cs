
using Microsoft.AspNetCore.Http.HttpResults;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Errors;
using TodoHelper.Domain.Results;
using _Todo = TodoHelper.Domain.Entities.Todo;
using DeleteTodo = TodoHelper.Application.Features.Todo.Delete;

namespace TodoHelper.Application.Features.Todo.Delete;

internal static class EndpointExtension
{
    internal static WebApplication MapDeleteTodo(this WebApplication app)
    {
        _ = app.MapDelete
            (
                pattern: "/todo/{id:guid}",
                handler: async Task<Results<InternalServerError<string>, NotFound<string>,
                    NoContent>>
                    (IRepository<_Todo> repository, DeleteTodo.Handler handler, Guid id) =>
                    {
                        DeleteTodo.Command command = new(id);
                        Result<DeleteTodo.Response> result = await handler.HandleAsync(command);
                        return result.IsFailure && result.Error is Error error && error.ErrorCode == ErrorCode.NotFound
                            ? TypedResults.NotFound(error.Description)
                            : result.IsSuccess && result.Payload is DeleteTodo.Response response
                                ? TypedResults.NoContent()
                                : TypedResults.InternalServerError(Error.Unknown.Description);
                    }
            );
        return app;
    }
}
