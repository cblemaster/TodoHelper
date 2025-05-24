
using Microsoft.AspNetCore.Http.HttpResults;
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Errors;
using _Category = TodoHelper.Domain.Entities.Category;
using GetCategories = TodoHelper.Application.Features.Category.GetAll;

namespace TodoHelper.Application.Features.Category.GetAll;

internal static class EndpointExtension
{
    internal static WebApplication MapGetAllCategory(this WebApplication app)
    {
        _ = app.MapGet
            (
                pattern: "/category",
                handler: async Task<Results<InternalServerError<string>, Ok<IEnumerable<CategoryDTO>>>>
                    (IRepository<_Category> repository, GetCategories.Handler handler) =>
                    {
                        GetCategories.Command command = new();
                        GetCategories.Response response = await handler.HandleAsync(command);
                        return response.Categories.IsFailure &&
                            response.Categories.Error is not null
                                ? TypedResults.InternalServerError(Error.Unknown.Description)
                                : response.Categories.IsSuccess &&
                                    response.Categories.Payload is IEnumerable<CategoryDTO> dtos
                                        ? TypedResults.Ok(dtos)
                                        : TypedResults.InternalServerError(Error.Unknown.Description);
                    }
            );
        return app;
    }
}
