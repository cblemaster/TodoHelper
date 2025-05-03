
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TodoHelper.Application.Features.CreateCategory;
using TodoHelper.Application.Features.CreateTodo;
using TodoHelper.Application.Features.GetCategories;
using TodoHelper.Application.Features.GetTodosCompleted;
using TodoHelper.Application.Features.GetTodosDueToday;
using TodoHelper.Application.Features.GetTodosForCategory;
using TodoHelper.Application.Features.GetTodosImportant;
using TodoHelper.Application.Features.GetTodosOverdue;
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
builder.Services.AddScoped<ICommandHandler<GetTodosCompletedCommand, GetTodosCompletedResponse>, GetTodosCompletedHandler>();
builder.Services.AddScoped<ICommandHandler<GetTodosDueTodayCommand, GetTodosDueTodayResponse>, GetTodosDueTodayHandler>();
builder.Services.AddScoped<ICommandHandler<GetTodosForCategoryCommand, GetTodosForCategoryResponse>, GetTodosForCategoryHandler>();
builder.Services.AddScoped<ICommandHandler<GetTodosImportantCommand, GetTodosImportantResponse>, GetTodosImportantHandler>();
builder.Services.AddScoped<ICommandHandler<GetTodosOverdueCommand, GetTodosOverdueResponse>, GetTodosOverdueHandler>();


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
    handler: async Task<Results<Ok<IOrderedEnumerable<Category>>, InternalServerError<string>>>
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
app.MapGet(pattern: "/category/{id:guid}/todo",
    handler: async Task<Results<Ok<IOrderedEnumerable<Todo>>, InternalServerError<string>>>
    (Guid id, ICommandHandler<GetTodosForCategoryCommand, GetTodosForCategoryResponse> handler) =>
    {
        GetTodosForCategoryCommand command = new(id);

        Result<GetTodosForCategoryResponse> response = await handler.HandleAsync(command);
        if (response.IsSuccess && response.Value is not null && response.Value.TodosForCategory is IOrderedEnumerable<Todo> todos)
        {
            return TypedResults.Ok(todos);
        }
        else
        {
            return TypedResults.InternalServerError("An unknown error occurred when getting todos.");
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
app.MapGet(pattern: "/todo/complete",
    handler: async Task<Results<Ok<IOrderedEnumerable<Todo>>, InternalServerError<string>>>
    (ICommandHandler<GetTodosCompletedCommand, GetTodosCompletedResponse> handler) =>
    {
        GetTodosCompletedCommand command = new();

        Result<GetTodosCompletedResponse> response = await handler.HandleAsync(command);
        if (response.IsSuccess && response.Value is not null && response.Value.CompleteTodos is IOrderedEnumerable<Todo> todos)
        {
            return TypedResults.Ok(todos);
        }
        else
        {
            return TypedResults.InternalServerError("An unknown error occurred when getting todos.");
        }
    });
app.MapGet(pattern: "/todo/duetoday",
    handler: async Task<Results<Ok<IOrderedEnumerable<Todo>>, InternalServerError<string>>>
    (ICommandHandler<GetTodosDueTodayCommand, GetTodosDueTodayResponse> handler) =>
    {
        GetTodosDueTodayCommand command = new();

        Result<GetTodosDueTodayResponse> response = await handler.HandleAsync(command);
        if (response.IsSuccess && response.Value is not null && response.Value.DueTodayTodos is IOrderedEnumerable<Todo> todos)
        {
            return TypedResults.Ok(todos);
        }
        else
        {
            return TypedResults.InternalServerError("An unknown error occurred when getting todos.");
        }
    });
app.MapGet(pattern: "/todo/important",
    handler: async Task<Results<Ok<IOrderedEnumerable<Todo>>, InternalServerError<string>>>
    (ICommandHandler<GetTodosImportantCommand, GetTodosImportantResponse> handler) =>
    {
        GetTodosImportantCommand command = new();

        Result<GetTodosImportantResponse> response = await handler.HandleAsync(command);
        if (response.IsSuccess && response.Value is not null && response.Value.ImportantTodos is IOrderedEnumerable<Todo> todos)
        {
            return TypedResults.Ok(todos);
        }
        else
        {
            return TypedResults.InternalServerError("An unknown error occurred when getting todos.");
        }
    });
app.MapGet(pattern: "/todo/overdue",
    handler: async Task<Results<Ok<IOrderedEnumerable<Todo>>, InternalServerError<string>>>
    (ICommandHandler<GetTodosOverdueCommand, GetTodosOverdueResponse> handler) =>
    {
        GetTodosOverdueCommand command = new();

        Result<GetTodosOverdueResponse> response = await handler.HandleAsync(command);
        if (response.IsSuccess && response.Value is not null && response.Value.OverdueTodos is IOrderedEnumerable<Todo> todos)
        {
            return TypedResults.Ok(todos);
        }
        else
        {
            return TypedResults.InternalServerError("An unknown error occurred when getting todos.");
        }
    });

app.Run();
