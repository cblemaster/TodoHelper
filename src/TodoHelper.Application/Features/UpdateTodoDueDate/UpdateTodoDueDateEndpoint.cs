
using Microsoft.AspNetCore.Http.HttpResults;
using TodoHelper.Application.Features.Common.Specifications;
using TodoHelper.Application.Interfaces;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.UpdateTodoDueDate;

internal static class UpdateTodoDueDateEndpoint
{
    internal static RouteHandlerBuilder MapUpdateTodoDueDateEndpoint(this WebApplication app) => app.MapPut(pattern: "/todo/{id:guid}/duedate",
    handler: async Task<Results<NoContent, NotFound<string>, BadRequest<string>, InternalServerError<string>>>
    (Guid id, UpdateTodoDueDateCommand command, ICommandHandler<UpdateTodoDueDateCommand, UpdateTodoDueDateResponse> handler) =>
    {
        Result<UpdateTodoDueDateResponse> response = await handler.HandleAsync(command);
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
