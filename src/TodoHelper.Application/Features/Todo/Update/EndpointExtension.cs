
using Microsoft.AspNetCore.Http.HttpResults;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Errors;
using TodoHelper.Domain.Results;
using _Todo = TodoHelper.Domain.Entities.Todo;
using UpdateTodo = TodoHelper.Application.Features.Todo.Update;

namespace TodoHelper.Application.Features.Todo.Update;

internal static class EndpointExtension
{
    internal static WebApplication MapUpdateTodo(this WebApplication app)
    {
        _ = app.MapPut
            (
                pattern: "/todo/{id:guid}",
                handler: async Task<Results<InternalServerError<string>, NotFound<string>,
                    BadRequest<string>, NoContent>>
                    (IRepository<_Todo> repository, UpdateTodo.Command command, UpdateTodo.Handler handler, Guid id) =>
                    {
                        Result<UpdateTodo.Response> result = await handler.HandleAsync(command);
                        return result.IsFailure && result.Error is Error error
                            ? error.ErrorCode == ErrorCode.NotFound
                                ? TypedResults.NotFound(error.Description)
                                : error.ErrorCode == ErrorCode.NotValid
                                    ? TypedResults.BadRequest(error.Description)
                                    : TypedResults.InternalServerError(Error.Unknown.Description)
                            : result.IsSuccess && result.Value is UpdateTodo.Response response
                                ? TypedResults.NoContent()
                                : TypedResults.InternalServerError(Error.Unknown.Description);
                    }
            );
        return app;
    }
}
