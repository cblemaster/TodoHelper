
using Microsoft.AspNetCore.Http.HttpResults;
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Application.Features.Common.Specifications;
using TodoHelper.Application.Interfaces;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.GetCategories;

internal static class GetCategoriesEndpoint
{
    internal static RouteHandlerBuilder MapGetCategoriesEndpoint(this WebApplication app) => app.MapGet(pattern: "/category",
    handler: async Task<Results<Ok<ICollection<CategoryDTO>>, InternalServerError<string>>>
    (ICommandHandler<GetCategoriesCommand, GetCategoriesResponse> handler) =>
    {
        GetCategoriesCommand command = new();
        Result<GetCategoriesResponse> response = await handler.HandleAsync(command);
        return response.IsSuccess && response.Value is not null && response.Value.Categories is ICollection<CategoryDTO> categories
            ? TypedResults.Ok(categories)
            : TypedResults.InternalServerError(ApplicationErrors.UnknownErrorMessage("getting categories"));
    });
}
