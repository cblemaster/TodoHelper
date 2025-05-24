
using Microsoft.AspNetCore.Http.HttpResults;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Errors;
using TodoHelper.Domain.Results;
using _Category = TodoHelper.Domain.Entities.Category;
using DeleteCategory = TodoHelper.Application.Features.Category.Delete;

namespace TodoHelper.Application.Features.Category.Delete;

internal static class EndpointExtension
{
    internal static WebApplication MapDeleteCategory(this WebApplication app)
    {
        _ = app.MapDelete
            (
                pattern: "/category/{id:guid}",
                handler: async Task<Results<InternalServerError<string>, NotFound<string>,
                    NoContent>>
                    (IRepository<_Category> repository, DeleteCategory.Handler handler, Guid id) =>
                    {
                        DeleteCategory.Command command = new(id);
                        Response response = await handler.HandleAsync(command);
                        return response.Result.IsFailure && response.Result.Error is Error error && error.ErrorCode == ErrorCode.NotFound
                            ? TypedResults.NotFound(error.Description)
                            : response.Result.IsSuccess
                                ? TypedResults.NoContent()
                                : TypedResults.InternalServerError(Error.Unknown.Description);
                    }
            );
        return app;
    }
}
