
using Microsoft.AspNetCore.Http.HttpResults;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Errors;
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
                        Response response = await handler.HandleAsync(command);
                        return response.Result.IsFailure &&
                            response.Result.Error is Error error &&
                            error.ErrorCode == ErrorCode.NotFound
                                ? TypedResults.NotFound(error.Description)
                                : response.Result.IsSuccess
                                ? TypedResults.NoContent()
                                : TypedResults.InternalServerError(Error.Unknown.Description);
                    }
            );
        return app;
    }
}
