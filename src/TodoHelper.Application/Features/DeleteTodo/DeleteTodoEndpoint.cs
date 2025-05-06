
using Microsoft.AspNetCore.Http.HttpResults;
using TodoHelper.Application.Features.Common.Specifications;
using TodoHelper.Application.Interfaces;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.DeleteTodo;

internal static class DeleteTodoEndpoint
{
    internal static RouteHandlerBuilder MapDeleteTodoEndpoint(this WebApplication app) => app.MapDelete(pattern: "/todo/{id:guid}",
    handler: async Task<Results<NoContent, NotFound<string>, BadRequest<string>, InternalServerError<string>>>
    (Guid id, ICommandHandler<DeleteTodoCommand, DeleteTodoResponse> handler) =>
    {
        DeleteTodoCommand command = new(id);

        Result<DeleteTodoResponse> response = await handler.HandleAsync(command);
        if (response.IsSuccess && response.Value is not null && response.Value.IsSuccess)
        {
            return TypedResults.NoContent();
        }
        else if (response.IsFailure && response.Error is string error)
        {
            // TODO: this check is brittle...
            return error.Contains("not found") ? TypedResults.NotFound(error) : TypedResults.BadRequest(error);
        }
        else
        {
            return TypedResults.InternalServerError(ApplicationErrors.UnknownErrorMessage("deleting todo"));
        }
    });
}
