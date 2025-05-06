
using Microsoft.AspNetCore.Http.HttpResults;
using TodoHelper.Application.Features.Common.Specifications;
using TodoHelper.Application.Interfaces;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.UpdateTodoImportance;

internal static class UpdateTodoImportanceEndpoint
{
    internal static WebApplication MapUpdateTodoImportanceEndpoint(this WebApplication app)
    {
        app.MapPut(
            pattern: "/todo/{id:guid}/importance",
            handler: async Task<Results<NoContent, NotFound<string>, BadRequest<string>, InternalServerError<string>>>
                (Guid id, UpdateTodoImportanceCommand command, ICommandHandler<UpdateTodoImportanceCommand, UpdateTodoImportanceResponse> handler) =>
                {
                    Result<UpdateTodoImportanceResponse> response = await handler.HandleAsync(command);
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
                }
            );
        return app;
    }
}
