
using Microsoft.AspNetCore.Http.HttpResults;
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.BaseClasses;
using TodoHelper.Domain.Errors;
using TodoHelper.Domain.Results;
using _Category = TodoHelper.Domain.Entities.Category;
using _Todo = TodoHelper.Domain.Entities.Todo;
using GetTodos = TodoHelper.Application.Features.Todo.GetAllByCategory;

namespace TodoHelper.Application.Features.Todo.GetAllByCategory;

internal static class EndpointExtension
{
    internal static WebApplication MapGetAllTodoByCategory(this WebApplication app)
    {
        _ = app.MapGet
            (
                pattern: "category/{id:guid}/todo",
                handler: async Task<Results<InternalServerError<string>, Ok<IEnumerable<TodoDTO>>>>
                (IRepository<_Todo> repository, GetTodos.Handler handler, Guid id) =>
                {
                    GetTodos.Command command = new(Identifier<_Category>.Create(id), true);
                    GetTodos.Response response = await handler.HandleAsync(command);
                    return response.Todos.IsFailure && response.Todos.Error is not null
                        ? TypedResults.InternalServerError(Error.Unknown.Description)
                        : response.Todos.IsSuccess && response.Todos.Payload is IEnumerable<TodoDTO> dtos
                            ? TypedResults.Ok(dtos)
                            : TypedResults.InternalServerError(Error.Unknown.Description);
                }
            );
        return app;
    }
}
