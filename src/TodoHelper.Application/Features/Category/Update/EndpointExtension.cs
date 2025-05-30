﻿
using Microsoft.AspNetCore.Http.HttpResults;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Errors;
using _Category = TodoHelper.Domain.Entities.Category;
using UpdateCategory = TodoHelper.Application.Features.Category.Update;

namespace TodoHelper.Application.Features.Category.Update;

internal static class EndpointExtension
{
    internal static WebApplication MapUpdateCategory(this WebApplication app)
    {
        _ = app.MapPut
            (
                pattern: "/category/{id:guid}",
                handler: async Task<Results<InternalServerError<string>, NotFound<string>,
                    BadRequest<string>, NoContent>>
                    (IRepository<_Category> repository, UpdateCategory.Command command,
                        UpdateCategory.Handler handler, Guid id) =>
                        {
                            Response response = await handler.HandleAsync(command);
                            return response.Result.IsFailure &&
                                response.Result.Error is Error error
                                    ? error.ErrorCode == ErrorCode.NotFound
                                        ? TypedResults.NotFound(error.Description)
                                        : error.ErrorCode == ErrorCode.NotValid
                                            ? TypedResults.BadRequest(error.Description)
                                            : TypedResults.InternalServerError
                                                (Error.Unknown.Description)
                                    : response.Result.IsSuccess
                                        ? TypedResults.NoContent()
                                        : TypedResults.InternalServerError
                                            (Error.Unknown.Description);
                            }
                    );
        return app;
    }
}
