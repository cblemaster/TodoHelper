
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.DataAccess.Context;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Errors;
using TodoHelper.Domain.Results;
using Create = TodoHelper.Application.Features.Category.Create;
using GetAll = TodoHelper.Application.Features.Category.GetAll;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string conn = builder.Configuration.GetConnectionString("todo-helper-conn") ?? "Error getting connection string.";
builder.Services.AddDbContext<TodosDbContext>(options => options.UseSqlServer(conn));

builder.Services.AddScoped<ITodosRepository<Category>, TodosRepository<Category>>();
builder.Services.AddScoped<ITodosRepository<Todo>, TodosRepository<Todo>>();
builder.Services.AddScoped<Create.Handler>();
builder.Services.AddScoped<GetAll.Handler>();

WebApplication app = builder.Build();

app.MapGet("/", () => "Welcome!");

app.MapPost(pattern: "/category",
    handler: async Task<Results<BadRequest<string>, Created<CategoryDTO>, InternalServerError<string>>>
    (ITodosRepository<Category> repository, Create.Command command, Create.Handler handler) =>
    {
        Result<Create.Response> result = await handler.HandleAsync(command);
        return result.IsFailure && result.Error is Error error
            ? TypedResults.BadRequest(error.Description)
            : result.IsSuccess && result.Value is not null and Create.Response response
                ? TypedResults.Created("no uri for this resource", response.Category)
                : TypedResults.InternalServerError(Error.Unknown.Description);
    });
app.MapGet(pattern: "/category",
    handler: async Task<Results<InternalServerError<string>, Ok<IEnumerable<CategoryDTO>>>>
    (ITodosRepository<Category> repository, GetAll.Handler handler) =>
    {
        GetAll.Command command = new();
        Result<GetAll.Response> result = await handler.HandleAsync(command);
        return result.IsFailure && result.Error is Error error
            ? TypedResults.InternalServerError(error.Description)
            : result.IsSuccess && result.Value is not null and GetAll.Response response
                ? TypedResults.Ok(response.Categories)
                : TypedResults.InternalServerError(Error.Unknown.Description);
    });

app.Run();
