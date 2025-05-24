
using Microsoft.AspNetCore.Http.HttpResults;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Errors;
using _Todo = TodoHelper.Domain.Entities.Todo;
using UpdateTodo = TodoHelper.Application.Features.Todo.UpdateCompleteDate;

namespace TodoHelper.Application.Features.Todo.UpdateCompleteDate;

public static class EndpointExtension
{
    internal static WebApplication MapUpdateTodoCompleteDate(this WebApplication app)
    {
        _ = app.MapPut
            (
                pattern: "/todo/{id:guid}",
                handler: async Task<Results<InternalServerError<string>, NotFound<string>,
                    BadRequest<string>, NoContent>>
                    (IRepository<_Todo> repository, UpdateTodo.Command command,
                        UpdateTodo.Handler handler, Guid id) =>
                        {
                            Response response = await handler.HandleAsync(command);
                            return response.Result.IsFailure &&
                                response.Result.Error is Error error
                                    ? error.ErrorCode == ErrorCode.NotFound
                                        ? TypedResults.NotFound(error.Description)
                                        : error.ErrorCode == ErrorCode.NotValid
                                            ? TypedResults.BadRequest(error.Description)
                                            : TypedResults.InternalServerError
                                                (Error.Unknown.Description)
                                    : response.Result.IsSuccess
                                        ? TypedResults.NoContent()
                                        : TypedResults.InternalServerError
                                            (Error.Unknown.Description);
                            }
                        );
        return app;
    }
}
