
using Microsoft.AspNetCore.Http.HttpResults;
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Errors;
using TodoHelper.Domain.Results;
using _Category = TodoHelper.Domain.Entities.Category;
using GetCategory = TodoHelper.Application.Features.Category.Get;

namespace TodoHelper.Application.Features.Category.Get;

internal static class EndpointExtension
{
    internal static WebApplication MapGetCategory(this WebApplication app)
    {
        _ = app.MapGet
            (
                pattern: "/category/{id:guid}",
                handler: async Task<Results<InternalServerError<string>, NotFound<string>,
                    Ok<CategoryDTO>>>
                    (IRepository<_Category> repository, GetCategory.Handler handler, Guid id) =>
                    {
                        GetCategory.Command command = new(id);
                        Response response = await handler.HandleAsync(command);
                        return response.Category.IsFailure && response.Category.Error is Error error && error.ErrorCode == ErrorCode.NotFound
                            ? TypedResults.NotFound(error.Description)
                            : response.Category.IsSuccess && response.Category.Payload is CategoryDTO dto
                                ? TypedResults.Ok(dto)
                                : TypedResults.InternalServerError(Error.Unknown.Description);
                    }
            );
        return app;
    }
}
