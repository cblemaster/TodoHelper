
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.DataAccess.Context;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Errors;
using TodoHelper.Domain.Results;
using CreateCategory = TodoHelper.Application.Features.Category.Create;
using GetCategories = TodoHelper.Application.Features.Category.GetAll;
using GetCategory = TodoHelper.Application.Features.Category.Get;
using DeleteCategory = TodoHelper.Application.Features.Category.Delete;
using UpdateCategory = TodoHelper.Application.Features.Category.Update;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string conn = builder.Configuration.GetConnectionString("todo-helper-conn") ?? "Error getting connection string.";
builder.Services.AddDbContext<TodosDbContext>(options => options.UseSqlServer(conn));

builder.Services.AddScoped<IRepository<Category>, Repository<Category>>();
builder.Services.AddScoped<IRepository<Todo>, Repository<Todo>>();
builder.Services.AddScoped<CreateCategory.Handler>();
builder.Services.AddScoped<GetCategories.Handler>();
builder.Services.AddScoped<GetCategory.Handler>();
builder.Services.AddScoped<UpdateCategory.Handler>();
builder.Services.AddScoped<DeleteCategory.Handler>();

WebApplication app = builder.Build();

app.MapGet("/", () => "Welcome!");

#region Category endpoints
app.MapPost(pattern: "/category",
    handler: async Task<Results<BadRequest<string>, Created<CategoryDTO>, InternalServerError<string>>>
    (IRepository<Category> repository, CreateCategory.Command command, CreateCategory.Handler handler) =>
    {
        Result<CreateCategory.Response> result = await handler.HandleAsync(command);
        return result.IsFailure && result.Error is Error error && error.ErrorCode == ErrorCode.NotValid
            ? TypedResults.BadRequest(error.Description)
            : result.IsSuccess && result.Value is CreateCategory.Response response
                ? TypedResults.Created("no uri for this resource", response.Category)
                : TypedResults.InternalServerError(Error.Unknown.Description);
    });
app.MapGet(pattern: "/category",
    handler: async Task<Results<InternalServerError<string>, Ok<IEnumerable<CategoryDTO>>>>
    (IRepository<Category> repository, GetCategories.Handler handler) =>
    {
        GetCategories.Command command = new();
        Result<GetCategories.Response> result = await handler.HandleAsync(command);
        return result.IsFailure && result.Error is not null
            ? TypedResults.InternalServerError(Error.Unknown.Description)
            : result.IsSuccess && result.Value is GetCategories.Response response
                ? TypedResults.Ok(response.Categories)
                : TypedResults.InternalServerError(Error.Unknown.Description);
    });
app.MapGet(pattern: "/category/{id:guid}",
    handler: async Task<Results<InternalServerError<string>, NotFound<string>, Ok<CategoryDTO>>>
    (IRepository<Category> repository, GetCategory.Handler handler, Guid id) =>
    {
        GetCategory.Command command = new(id);
        Result<GetCategory.Response> result = await handler.HandleAsync(command);
        return result.IsFailure && result.Error is Error error && error.ErrorCode == ErrorCode.NotFound
            ? TypedResults.NotFound(error.Description)
            : result.IsSuccess && result.Value is not null and GetCategory.Response response
                ? TypedResults.Ok(response.Category)
                : TypedResults.InternalServerError(Error.Unknown.Description);
    });
app.MapPut(pattern: "/category/{id:guid}",
    handler: async Task<Results<InternalServerError<string>, NotFound<string>, BadRequest<string>, NoContent>>
    (IRepository<Category> repository, UpdateCategory.Command command, UpdateCategory.Handler handler, Guid id) =>
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
    });
app.MapDelete(pattern: "/category/{id:guid}",
    handler: async Task<Results<InternalServerError<string>, NotFound<string>, NoContent>>
    (IRepository<Category> repository, DeleteCategory.Command command, DeleteCategory.Handler handler, Guid id) =>
    {
        Result<DeleteCategory.Response> result = await handler.HandleAsync(command);
        return result.IsFailure && result.Error is Error error && error.ErrorCode == ErrorCode.NotFound
            ? TypedResults.NotFound(error.Description)
            : result.IsSuccess && result.Value is DeleteCategory.Response response
                ? TypedResults.NoContent()
                : TypedResults.InternalServerError(Error.Unknown.Description);
    });
#endregion Category endpoints

app.Run();
