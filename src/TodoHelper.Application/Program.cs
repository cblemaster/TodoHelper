
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TodoHelper.Application;
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Application.Extensions;
using TodoHelper.Application.Features.CreateCategory;
using TodoHelper.Application.Features.CreateTodo;
using TodoHelper.Application.Features.DeleteCategory;
using TodoHelper.Application.Features.DeleteTodo;
using TodoHelper.Application.Features.GetCategories;
using TodoHelper.Application.Features.GetTodosCompleted;
using TodoHelper.Application.Features.GetTodosDueToday;
using TodoHelper.Application.Features.GetTodosForCategory;
using TodoHelper.Application.Features.GetTodosImportant;
using TodoHelper.Application.Features.GetTodosOverdue;
using TodoHelper.Application.Features.UpdateCategoryName;
using TodoHelper.Application.Features.UpdateTodoCategory;
using TodoHelper.Application.Features.UpdateTodoCompleteDate;
using TodoHelper.Application.Features.UpdateTodoDescription;
using TodoHelper.Application.Features.UpdateTodoDueDate;
using TodoHelper.Application.Features.UpdateTodoImportance;
using TodoHelper.Application.Interfaces;
using TodoHelper.DataAccess;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Results;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string conn = builder.Configuration.GetConnectionString("todo-helper-conn") ?? "Error getting connection string.";
builder.Services.AddDbContext<TodosDbContext>(options => options.UseSqlServer(conn));
builder.Services.AddScoped<ITodosRepository, TodosRepository>();
builder.RegisterHandlers();
WebApplication app = builder.Build();

app.MapGet("/", () => "Welcome!");

app.MapPost(pattern: "/category",
    handler: async Task<Results<BadRequest<string>, Created<CategoryDTO>, InternalServerError<string>>> (CreateCategoryCommand command,
    ICommandHandler<CreateCategoryCommand, CreateCategoryResponse> handler) =>
    {
        Result<CreateCategoryResponse> response = await handler.HandleAsync(command);

        if (response.IsFailure && response.Error is string error)
        {
            return TypedResults.BadRequest(error);
        }
        else if (response.IsSuccess && response.Value is not null && response.Value.Category is CategoryDTO category)
        {
            return TypedResults.Created("", category);     // TODO: fix the resource uri
        }
        else
        {
            return TypedResults.InternalServerError(ApplicationErrors.UnknownErrorMessage("creating category"));
        }
    });
app.MapPost(pattern: "/todo",
    handler: async Task<Results<BadRequest<string>, Created<TodoDTO>, InternalServerError<string>>> (CreateTodoCommand command,
    ICommandHandler<CreateTodoCommand, CreateTodoResponse> handler) =>
    {
        Result<CreateTodoResponse> response = await handler.HandleAsync(command);

        if (response.IsFailure && response.Error is string error)
        {
            return TypedResults.BadRequest(error);
        }
        else if (response.IsSuccess && response.Value is not null && response.Value.Todo is TodoDTO todo)
        {
            return TypedResults.Created("", todo);     // TODO: fix the resource uri
        }
        else
        {
            return TypedResults.InternalServerError(ApplicationErrors.UnknownErrorMessage("creating todo"));
        }
    });

app.MapGet(pattern: "/category",
    handler: async Task<Results<Ok<ICollection<CategoryDTO>>, InternalServerError<string>>>
    (ICommandHandler<GetCategoriesCommand, GetCategoriesResponse> handler) =>
    {
        GetCategoriesCommand command = new();
        Result<GetCategoriesResponse> response = await handler.HandleAsync(command);
        return response.IsSuccess && response.Value is not null && response.Value.Categories is ICollection<CategoryDTO> categories
            ? TypedResults.Ok(categories)
            : TypedResults.InternalServerError(ApplicationErrors.UnknownErrorMessage("getting categories"));
    });
app.MapGet(pattern: "/category/{id:guid}/todo",
    handler: async Task<Results<Ok<ICollection<TodoDTO>>, InternalServerError<string>>>
    (Guid id, ICommandHandler<GetTodosForCategoryCommand, GetTodosForCategoryResponse> handler) =>
    {
        GetTodosForCategoryCommand command = new(id);
        Result<GetTodosForCategoryResponse> response = await handler.HandleAsync(command);
        return response.IsSuccess && response.Value is not null && response.Value.TodosForCategory is ICollection<TodoDTO> todos
            ? TypedResults.Ok(todos)
            : TypedResults.InternalServerError(ApplicationErrors.UnknownErrorMessage("getting todos"));
    });
app.MapGet(pattern: "/todo/complete",
    handler: async Task<Results<Ok<ICollection<TodoDTO>>, InternalServerError<string>>>
    (ICommandHandler<GetTodosCompletedCommand, GetTodosCompletedResponse> handler) =>
    {
        GetTodosCompletedCommand command = new();
        Result<GetTodosCompletedResponse> response = await handler.HandleAsync(command);
        return response.IsSuccess && response.Value is not null && response.Value.CompleteTodos is ICollection<TodoDTO> todos
            ? TypedResults.Ok(todos)
            : TypedResults.InternalServerError(ApplicationErrors.UnknownErrorMessage("getting todos"));
    });
app.MapGet(pattern: "/todo/duetoday",
    handler: async Task<Results<Ok<ICollection<TodoDTO>>, InternalServerError<string>>>
    (ICommandHandler<GetTodosDueTodayCommand, GetTodosDueTodayResponse> handler) =>
    {
        GetTodosDueTodayCommand command = new();
        Result<GetTodosDueTodayResponse> response = await handler.HandleAsync(command);
        return response.IsSuccess && response.Value is not null && response.Value.DueTodayTodos is ICollection<TodoDTO> todos
            ? TypedResults.Ok(todos)
            : TypedResults.InternalServerError(ApplicationErrors.UnknownErrorMessage("getting todos"));
    });
app.MapGet(pattern: "/todo/important",
    handler: async Task<Results<Ok<ICollection<TodoDTO>>, InternalServerError<string>>>
    (ICommandHandler<GetTodosImportantCommand, GetTodosImportantResponse> handler) =>
    {
        GetTodosImportantCommand command = new();
        Result<GetTodosImportantResponse> response = await handler.HandleAsync(command);
        return response.IsSuccess && response.Value is not null && response.Value.ImportantTodos is ICollection<TodoDTO> todos
            ? TypedResults.Ok(todos)
            : TypedResults.InternalServerError(ApplicationErrors.UnknownErrorMessage("getting todos"));
    });
app.MapGet(pattern: "/todo/overdue",
    handler: async Task<Results<Ok<ICollection<TodoDTO>>, InternalServerError<string>>>
    (ICommandHandler<GetTodosOverdueCommand, GetTodosOverdueResponse> handler) =>
    {
        GetTodosOverdueCommand command = new();
        Result<GetTodosOverdueResponse> response = await handler.HandleAsync(command);
        return response.IsSuccess && response.Value is not null && response.Value.OverdueTodos is ICollection<TodoDTO> todos
            ? TypedResults.Ok(todos)
            : TypedResults.InternalServerError(ApplicationErrors.UnknownErrorMessage("getting todos"));
    });

app.MapPut(pattern: "/category/{id:guid}",
    handler: async Task<Results<NoContent, NotFound<string>, BadRequest<string>, InternalServerError<string>>>
    (Guid id, UpdateCategoryNameCommand command, ICommandHandler<UpdateCategoryNameCommand, UpdateCategoryNameResponse> handler) =>
    {
        Result<UpdateCategoryNameResponse> response = await handler.HandleAsync(command);
        if (response.IsSuccess && response.Value is not null && response.Value.IsSuccess)
        {
            return TypedResults.NoContent();
        }
        else if (response.IsFailure && response.Error is string error)
        {
            // TODO: this check is brittle...
            return error.Contains("not found") ? TypedResults.NotFound(error) : TypedResults.BadRequest(error);
        }
        else
        {
            return TypedResults.InternalServerError(ApplicationErrors.UnknownErrorMessage("updating category"));
        }
    });
app.MapPut(pattern: "/todo/{id:guid}/category",
    handler: async Task<Results<NoContent, NotFound<string>, BadRequest<string>, InternalServerError<string>>>
    (Guid id, UpdateTodoCategoryCommand command, ICommandHandler<UpdateTodoCategoryCommand, UpdateTodoCategoryResponse> handler) =>
    {
        Result<UpdateTodoCategoryResponse> response = await handler.HandleAsync(command);
        if (response.IsSuccess && response.Value is not null && response.Value.IsSuccess)
        {
            return TypedResults.NoContent();
        }
        else if (response.IsFailure && response.Error is string error)
        {
            // TODO: this check is brittle...
            return error.Contains("not found") ? TypedResults.NotFound(error) : TypedResults.BadRequest(error);
        }
        else
        {
            return TypedResults.InternalServerError(ApplicationErrors.UnknownErrorMessage("updating category"));
        }
    });
app.MapPut(pattern: "/todo/{id:guid}/completedate",
    handler: async Task<Results<NoContent, NotFound<string>, BadRequest<string>, InternalServerError<string>>>
    (Guid id, UpdateTodoCompleteDateCommand command, ICommandHandler<UpdateTodoCompleteDateCommand, UpdateTodoCompleteDateResponse> handler) =>
    {
        Result<UpdateTodoCompleteDateResponse> response = await handler.HandleAsync(command);
        if (response.IsSuccess && response.Value is not null && response.Value.IsSuccess)
        {
            return TypedResults.NoContent();
        }
        else if (response.IsFailure && response.Error is string error)
        {
            // TODO: this check is brittle...
            return error.Contains("not found") ? TypedResults.NotFound(error) : TypedResults.BadRequest(error);
        }
        else
        {
            return TypedResults.InternalServerError(ApplicationErrors.UnknownErrorMessage("updating category"));
        }
    });
app.MapPut(pattern: "/todo/{id:guid}/description",
    handler: async Task<Results<NoContent, NotFound<string>, BadRequest<string>, InternalServerError<string>>>
    (Guid id, UpdateTodoDescriptionCommand command, ICommandHandler<UpdateTodoDescriptionCommand, UpdateTodoDescriptionResponse> handler) =>
    {
        Result<UpdateTodoDescriptionResponse> response = await handler.HandleAsync(command);
        if (response.IsSuccess && response.Value is not null && response.Value.IsSuccess)
        {
            return TypedResults.NoContent();
        }
        else if (response.IsFailure && response.Error is string error)
        {
            // TODO: this check is brittle...
            return error.Contains("not found") ? TypedResults.NotFound(error) : TypedResults.BadRequest(error);
        }
        else
        {
            return TypedResults.InternalServerError(ApplicationErrors.UnknownErrorMessage("updating category"));
        }
    });
app.MapPut(pattern: "/todo/{id:guid}/duedate",
    handler: async Task<Results<NoContent, NotFound<string>, BadRequest<string>, InternalServerError<string>>>
    (Guid id, UpdateTodoDueDateCommand command, ICommandHandler<UpdateTodoDueDateCommand, UpdateTodoDueDateResponse> handler) =>
    {
        Result<UpdateTodoDueDateResponse> response = await handler.HandleAsync(command);
        if (response.IsSuccess && response.Value is not null && response.Value.IsSuccess)
        {
            return TypedResults.NoContent();
        }
        else if (response.IsFailure && response.Error is string error)
        {
            // TODO: this check is brittle...
            return error.Contains("not found") ? TypedResults.NotFound(error) : TypedResults.BadRequest(error);
        }
        else
        {
            return TypedResults.InternalServerError(ApplicationErrors.UnknownErrorMessage("updating category"));
        }
    });
app.MapPut(pattern: "/todo/{id:guid}/importance",
    handler: async Task<Results<NoContent, NotFound<string>, BadRequest<string>, InternalServerError<string>>>
    (Guid id, UpdateTodoImportanceCommand command, ICommandHandler<UpdateTodoImportanceCommand, UpdateTodoImportanceResponse> handler) =>
    {
        Result<UpdateTodoImportanceResponse> response = await handler.HandleAsync(command);
        if (response.IsSuccess && response.Value is not null && response.Value.IsSuccess)
        {
            return TypedResults.NoContent();
        }
        else if (response.IsFailure && response.Error is string error)
        {
            // TODO: this check is brittle...
            return error.Contains("not found") ? TypedResults.NotFound(error) : TypedResults.BadRequest(error);
        }
        else
        {
            return TypedResults.InternalServerError(ApplicationErrors.UnknownErrorMessage("updating category"));
        }
    });

app.MapDelete(pattern: "/category/{id:guid}",
    handler: async Task<Results<NoContent, NotFound<string>, BadRequest<string>, InternalServerError<string>>>
    (Guid id, ICommandHandler<DeleteCategoryCommand, DeleteCategoryResponse> handler) =>
    {
        DeleteCategoryCommand command = new(id);

        Result<DeleteCategoryResponse> response = await handler.HandleAsync(command);
        if (response.IsSuccess && response.Value is not null && response.Value.IsSuccess)
        {
            return TypedResults.NoContent();
        }
        else if (response.IsFailure && response.Error is string error)
        {
            // TODO: this check is brittle...
            return error.Contains("not found") ? TypedResults.NotFound(error) : TypedResults.BadRequest(error);
        }
        else
        {
            return TypedResults.InternalServerError(ApplicationErrors.UnknownErrorMessage("deleting category"));
        }
    });
app.MapDelete(pattern: "/todo/{id:guid}",
    handler: async Task<Results<NoContent, NotFound<string>, BadRequest<string>, InternalServerError<string>>>
    (Guid id, ICommandHandler<DeleteTodoCommand, DeleteTodoResponse> handler) =>
    {
        DeleteTodoCommand command = new(id);

        Result<DeleteTodoResponse> response = await handler.HandleAsync(command);
        if (response.IsSuccess && response.Value is not null && response.Value.IsSuccess)
        {
            return TypedResults.NoContent();
        }
        else if (response.IsFailure && response.Error is string error)
        {
            // TODO: this check is brittle...
            return error.Contains("not found") ? TypedResults.NotFound(error) : TypedResults.BadRequest(error);
        }
        else
        {
            return TypedResults.InternalServerError(ApplicationErrors.UnknownErrorMessage("deleting todo"));
        }
    });

app.Run();
