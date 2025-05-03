
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TodoHelper.Application.Features.CreateCategory;
using TodoHelper.Application.Features.CreateTodo;
using TodoHelper.Application.Features.GetCategories;
using TodoHelper.Application.Interfaces;
using TodoHelper.DataAccess;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Results;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string conn = builder.Configuration.GetConnectionString("todo-helper-conn") ?? "Error getting connection string.";
builder.Services.AddDbContext<TodosDbContext>(options => options.UseSqlServer(conn));
builder.Services.AddScoped<ITodosRepository, TodosRepository>();
builder.Services.AddScoped<ICommandHandler<CreateCategoryCommand, CreateCategoryResponse>, CreateCategoryHandler>();
builder.Services.AddScoped<ICommandHandler<CreateTodoCommand, CreateTodoResponse>, CreateTodoHandler>();
builder.Services.AddScoped<ICommandHandler<GetCategoriesCommand, GetCategoriesResponse>, GetCategoriesHandler>();

WebApplication app = builder.Build();

app.MapGet("/", () => "Welcome!");

app.MapPost(pattern: "/category",
    handler: async Task<Results<BadRequest<string>, Created<Category>, InternalServerError<string>>> (CreateCategoryCommand command,
    ICommandHandler<CreateCategoryCommand, CreateCategoryResponse> handler) =>
    {
        Result<CreateCategoryResponse> response = await handler.HandleAsync(command);
        if (response.IsFailure && response.Error is not null)
        {
            return TypedResults.BadRequest(response.Error);
        }
        else if (response.IsSuccess && response.Value is not null && response.Value.Category is Category c)
        {
            return TypedResults.Created("", c);     // TODO: fix the resource uri
        }
        else
        {
            return TypedResults.InternalServerError("An unknown error occurred when creating the category.");
        }
    });

app.MapGet(pattern: "/category",
    handler: async Task<Results<Ok<IOrderedEnumerable<Category>>, Created<Todo>, InternalServerError<string>>>
    (ICommandHandler<GetCategoriesCommand, GetCategoriesResponse> handler) =>
    {
        GetCategoriesCommand command = new();

        Result<GetCategoriesResponse> response = await handler.HandleAsync(command);
        if (response.IsSuccess && response.Value is not null && response.Value.Categories is IOrderedEnumerable<Category> categories)
        {
            return TypedResults.Ok(categories);
        }
        else
        {
            return TypedResults.InternalServerError("An unknown error occurred when getting categories.");
        }
    });

app.MapPost(pattern: "/todo",
    handler: async Task<Results<BadRequest<string>, Created<Todo>, InternalServerError<string>>> (CreateTodoCommand command,
    ICommandHandler<CreateTodoCommand, CreateTodoResponse> handler) =>
    {
        Result<CreateTodoResponse> response = await handler.HandleAsync(command);
        if (response.IsFailure && response.Error is not null)
        {
            return TypedResults.BadRequest(response.Error);
        }
        else if (response.IsSuccess && response.Value is not null && response.Value.Todo is Todo t)
        {
            return TypedResults.Created("", t);     // TODO: fix the resource uri
        }
        else
        {
            return TypedResults.InternalServerError("An unknown error occurred when creating the todo.");
        }
    });

app.Run();
