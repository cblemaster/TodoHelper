
using Microsoft.AspNetCore.Http.HttpResults;
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Errors;
using TodoHelper.Domain.Results;
using _Category = TodoHelper.Domain.Entities.Category;
using CreateCategory = TodoHelper.Application.Features.Category.Create;

namespace TodoHelper.Application.Features.Category.Create;

internal static class EndpointExtension
{
    internal static WebApplication MapCreateCategory(this WebApplication app)
    {
        _= app.MapPost
            (
                pattern: "/category",
                handler: async Task<Results<BadRequest<string>, Created<CategoryDTO>,
                    InternalServerError<string>>>
                    (IRepository<_Category> repository, CreateCategory.Command command,
                        CreateCategory.Handler handler) =>
                        {
                            Result<CreateCategory.Response> result = await handler.HandleAsync(command);
                            return result.IsFailure && result.Error is Error error && error.ErrorCode == ErrorCode.NotValid
                                ? TypedResults.BadRequest(error.Description)
                                : result.IsSuccess && result.Value is CreateCategory.Response response
                                    ? TypedResults.Created("no uri for this resource", response.Category)
                                    : TypedResults.InternalServerError(Error.Unknown.Description);
                        }
            );
        return app;
    }
}
