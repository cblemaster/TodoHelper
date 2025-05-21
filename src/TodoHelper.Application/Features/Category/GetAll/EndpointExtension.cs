
using Microsoft.AspNetCore.Http.HttpResults;
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Errors;
using TodoHelper.Domain.Results;
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
                handler: async Task<Results<InternalServerError<string>,
                    Ok<IEnumerable<CategoryDTO>>>>
                    (IRepository<_Category> repository, GetCategories.Handler handler) =>
                    {
                        GetCategories.Command command = new();
                        Result<GetCategories.Response> result = await handler.HandleAsync(command);
                        return result.IsFailure && result.Error is not null
                            ? TypedResults.InternalServerError(Error.Unknown.Description)
                            : result.IsSuccess && result.Payload is GetCategories.Response response
                                ? TypedResults.Ok(response.Categories)
                                : TypedResults.InternalServerError(Error.Unknown.Description);
                    }
            );
        return app;
    }
}
