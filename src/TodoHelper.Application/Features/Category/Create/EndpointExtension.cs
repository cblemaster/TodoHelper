﻿
using Microsoft.AspNetCore.Http.HttpResults;
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Errors;
using _Category = TodoHelper.Domain.Entities.Category;
using CreateCategory = TodoHelper.Application.Features.Category.Create;

namespace TodoHelper.Application.Features.Category.Create;

internal static class EndpointExtension
{
    internal static WebApplication MapCreateCategory(this WebApplication app)
    {
        _ = app.MapPost
            (
                pattern: "/category",
                handler: async Task<Results<BadRequest<string>, Created<CategoryDTO>,
                    InternalServerError<string>>>
                    (IRepository<_Category> repository, CreateCategory.Command command,
                        CreateCategory.Handler handler) =>
                        {
                            Response response = await handler.HandleAsync(command);
                            return response.Category.IsFailure && response.Category.Error is Error error && error.ErrorCode == ErrorCode.NotValid
                                ? TypedResults.BadRequest(error.Description)
                                : response.Category.IsSuccess && response.Category.Payload is CategoryDTO dto
                                    ? TypedResults.Created("no uri for this resource", dto)
                                    : TypedResults.InternalServerError(Error.Unknown.Description);
                        }
            );
        return app;
    }
}
