
using Microsoft.AspNetCore.Http.HttpResults;
using TodoHelper.Application.Features.Common.Specifications;
using TodoHelper.Application.Interfaces;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.DeleteCategory;

internal static class DeleteCategoryEndpoint
{
    internal static RouteHandlerBuilder MapDeleteCategoryEndpoint(this WebApplication app) => app.MapDelete(pattern: "/category/{id:guid}",
    handler: async Task<Results<NoContent, NotFound<string>, BadRequest<string>, InternalServerError<string>>>
    (Guid id, ICommandHandler<DeleteCategoryCommand, DeleteCategoryResponse> handler) =>
    {
        DeleteCategoryCommand command = new(id);

        Result<DeleteCategoryResponse> response = await handler.HandleAsync(command);
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
            return TypedResults.InternalServerError(ApplicationErrors.UnknownErrorMessage("deleting category"));
        }
    });
}
