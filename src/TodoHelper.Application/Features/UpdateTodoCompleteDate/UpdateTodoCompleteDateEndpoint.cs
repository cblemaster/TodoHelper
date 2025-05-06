
using Microsoft.AspNetCore.Http.HttpResults;
using TodoHelper.Application.Features.Common.Specifications;
using TodoHelper.Application.Interfaces;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.UpdateTodoCompleteDate;

internal static class UpdateTodoCompleteDateEndpoint
{
    internal static RouteHandlerBuilder MapUpdateTodoCompleteDateEndpoint(this WebApplication app) => app.MapPut(pattern: "/todo/{id:guid}/completedate",
    handler: async Task<Results<NoContent, NotFound<string>, BadRequest<string>, InternalServerError<string>>>
    (Guid id, UpdateTodoCompleteDateCommand command, ICommandHandler<UpdateTodoCompleteDateCommand, UpdateTodoCompleteDateResponse> handler) =>
    {
        Result<UpdateTodoCompleteDateResponse> response = await handler.HandleAsync(command);
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
            return TypedResults.InternalServerError(ApplicationErrors.UnknownErrorMessage("updating category"));
        }
    });
}
