
using Microsoft.AspNetCore.Http.HttpResults;
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Application.Features.Common.Specifications;
using TodoHelper.Application.Interfaces;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.CreateTodo;

internal static class CreateTodoEndpoint
{
    internal static RouteHandlerBuilder MapCreateTodoEndpoint(this WebApplication app) => app.MapPost(pattern: "/todo",
    handler: async Task<Results<BadRequest<string>, Created<TodoDTO>, InternalServerError<string>>> (CreateTodoCommand command,
    ICommandHandler<CreateTodoCommand, CreateTodoResponse> handler) =>
    {
        Result<CreateTodoResponse> response = await handler.HandleAsync(command);

        if (response.IsFailure && response.Error is string error)
        {
            return TypedResults.BadRequest(error);
        }
        else if (response.IsSuccess && response.Value is not null && response.Value.Todo is TodoDTO todo)
        {
            return TypedResults.Created("No uri for this resource.", todo);
        }
        else
        {
            return TypedResults.InternalServerError(ApplicationErrors.UnknownErrorMessage("creating todo"));
        }
    });
}
