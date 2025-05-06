
using Microsoft.AspNetCore.Http.HttpResults;
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Application.Features.Common.Specifications;
using TodoHelper.Application.Interfaces;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.CreateCategory;

internal static class CreateCategoryEndpoint
{
    internal static RouteHandlerBuilder MapCreateCategoryEndpoint(this WebApplication app) => app.MapPost(pattern: "/category",
    handler: async Task<Results<BadRequest<string>, Created<CategoryDTO>, InternalServerError<string>>> (CreateCategoryCommand command,
    ICommandHandler<CreateCategoryCommand, CreateCategoryResponse> handler) =>
    {
        Result<CreateCategoryResponse> response = await handler.HandleAsync(command);

        if (response.IsFailure && response.Error is string error)
        {
            return TypedResults.BadRequest(error);
        }
        else if (response.IsSuccess && response.Value is not null && response.Value.Category is CategoryDTO category)
        {
            return TypedResults.Created("No uri for this resource.", category);
        }
        else
        {
            return TypedResults.InternalServerError(ApplicationErrors.UnknownErrorMessage("creating category"));
        }
    });
}
