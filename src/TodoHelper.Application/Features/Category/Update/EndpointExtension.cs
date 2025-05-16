
using Microsoft.AspNetCore.Http.HttpResults;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Errors;
using TodoHelper.Domain.Results;
using UpdateCategory = TodoHelper.Application.Features.Category.Update;
using _Category = TodoHelper.Domain.Entities.Category;

namespace TodoHelper.Application.Features.Category.Update;

internal static class EndpointExtension
{
    internal static WebApplication MapCategoryUpdate(this WebApplication app)
    {
        _ = app.MapPut
            (
                pattern: "/category/{id:guid}",
                handler: async Task<Results<InternalServerError<string>, NotFound<string>, BadRequest<string>, NoContent>>
                (IRepository<_Category> repository, UpdateCategory.Command command,
                    UpdateCategory.Handler handler, Guid id) =>
                    {
                        Result<UpdateCategory.Response> result = await handler.HandleAsync(command);
                        return result.IsFailure && result.Error is Error error
                            ? error.ErrorCode == ErrorCode.NotFound
                                ? TypedResults.NotFound(error.Description)
                                : error.ErrorCode == ErrorCode.NotValid
                                    ? TypedResults.BadRequest(error.Description)
                                    : TypedResults.InternalServerError(Error.Unknown.Description)
                            : result.IsSuccess && result.Value is UpdateCategory.Response response
                                ? TypedResults.NoContent()
                                : TypedResults.InternalServerError(Error.Unknown.Description);
                    }
            );
        return app;
    }
}
