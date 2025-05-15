
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
using CreateTodo = TodoHelper.Application.Features.Todo.Create;
using GetTodos = TodoHelper.Application.Features.Todo.GetAll;
using GetTodo = TodoHelper.Application.Features.Todo.Get;
using DeleteTodo = TodoHelper.Application.Features.Todo.Delete;
using UpdateTodo = TodoHelper.Application.Features.Todo.Update;

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

builder.Services.AddScoped<CreateTodo.Handler>();
builder.Services.AddScoped<GetTodos.Handler>();
builder.Services.AddScoped<GetTodo.Handler>();
builder.Services.AddScoped<UpdateTodo.Handler>();
builder.Services.AddScoped<DeleteTodo.Handler>();

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
    (IRepository<Category> repository, DeleteCategory.Handler handler, Guid id) =>
    {
        DeleteCategory.Command command = new(id);
        Result<DeleteCategory.Response> result = await handler.HandleAsync(command);
        return result.IsFailure && result.Error is Error error && error.ErrorCode == ErrorCode.NotFound
            ? TypedResults.NotFound(error.Description)
            : result.IsSuccess && result.Value is DeleteCategory.Response response
                ? TypedResults.NoContent()
                : TypedResults.InternalServerError(Error.Unknown.Description);
    });
#endregion Category endpoints

#region Todo endpoints
app.MapPost(pattern: "/todo",
    handler: async Task<Results<BadRequest<string>, Created<TodoDTO>, InternalServerError<string>>>
    (IRepository<Todo> repository, CreateTodo.Command command, CreateTodo.Handler handler) =>
    {
        Result<CreateTodo.Response> result = await handler.HandleAsync(command);
        return result.IsFailure && result.Error is Error error && error.ErrorCode == ErrorCode.NotValid
            ? TypedResults.BadRequest(error.Description)
            : result.IsSuccess && result.Value is CreateTodo.Response response
                ? TypedResults.Created("no uri for this resource", response.Todo)
                : TypedResults.InternalServerError(Error.Unknown.Description);
    });
app.MapGet(pattern: "/todo",
    handler: async Task<Results<InternalServerError<string>, Ok<IEnumerable<TodoDTO>>>>
    (IRepository<Todo> repository, GetTodos.Handler handler) =>
    {
        GetTodos.Command command = new();
        Result<GetTodos.Response> result = await handler.HandleAsync(command);
        return result.IsFailure && result.Error is not null
            ? TypedResults.InternalServerError(Error.Unknown.Description)
            : result.IsSuccess && result.Value is GetTodos.Response response
                ? TypedResults.Ok(response.Todos)
                : TypedResults.InternalServerError(Error.Unknown.Description);
    });
app.MapGet(pattern: "/todo/{id:guid}",
    handler: async Task<Results<InternalServerError<string>, NotFound<string>, Ok<TodoDTO>>>
    (IRepository<Todo> repository, GetTodo.Handler handler, Guid id) =>
    {
        GetTodo.Command command = new(id);
        Result<GetTodo.Response> result = await handler.HandleAsync(command);
        return result.IsFailure && result.Error is Error error && error.ErrorCode == ErrorCode.NotFound
            ? TypedResults.NotFound(error.Description)
            : result.IsSuccess && result.Value is not null and GetTodo.Response response
                ? TypedResults.Ok(response.Todo)
                : TypedResults.InternalServerError(Error.Unknown.Description);
    });
app.MapPut(pattern: "/todo/{id:guid}",
    handler: async Task<Results<InternalServerError<string>, NotFound<string>, BadRequest<string>, NoContent>>
    (IRepository<Todo> repository, UpdateTodo.Command command, UpdateTodo.Handler handler, Guid id) =>
    {
        Result<UpdateTodo.Response> result = await handler.HandleAsync(command);
        return result.IsFailure && result.Error is Error error
            ? error.ErrorCode == ErrorCode.NotFound
                ? TypedResults.NotFound(error.Description)
                : error.ErrorCode == ErrorCode.NotValid
                    ? TypedResults.BadRequest(error.Description)
                    : TypedResults.InternalServerError(Error.Unknown.Description)
            : result.IsSuccess && result.Value is UpdateTodo.Response response
                ? TypedResults.NoContent()
                : TypedResults.InternalServerError(Error.Unknown.Description);
    });
app.MapDelete(pattern: "/todo/{id:guid}",
    handler: async Task<Results<InternalServerError<string>, NotFound<string>, NoContent>>
    (IRepository<Todo> repository, DeleteTodo.Handler handler, Guid id) =>
    {
        DeleteTodo.Command command = new(id);
        Result<DeleteTodo.Response> result = await handler.HandleAsync(command);
        return result.IsFailure && result.Error is Error error && error.ErrorCode == ErrorCode.NotFound
            ? TypedResults.NotFound(error.Description)
            : result.IsSuccess && result.Value is DeleteTodo.Response response
                ? TypedResults.NoContent()
                : TypedResults.InternalServerError(Error.Unknown.Description);
    });
#endregion Todo endpoints

app.Run();
