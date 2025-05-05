
using Microsoft.EntityFrameworkCore;
using TodoHelper.Application;
using TodoHelper.DataAccess;
using TodoHelper.DataAccess.Repository;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string conn = builder.Configuration.GetConnectionString("todo-helper-conn") ?? "Error getting connection string.";
builder.Services.AddDbContext<TodosDbContext>(options => options.UseSqlServer(conn));
builder.Services.AddScoped<ITodosRepository, TodosRepository>();
builder.RegisterHandlers();
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
        return response.IsSuccess && response.Value is not null && response.Value.Categories is IOrderedEnumerable<Category> categories
            ? TypedResults.Ok(categories)
            : TypedResults.InternalServerError("An unknown error occurred when getting categories.");
    });
app.MapGet(pattern: "/category/{id:guid}/todo",
    handler: async Task<Results<Ok<IOrderedEnumerable<Todo>>, InternalServerError<string>>>
    (Guid id, ICommandHandler<GetTodosForCategoryCommand, GetTodosForCategoryResponse> handler) =>
    {
        GetTodosForCategoryCommand command = new(id);

        Result<GetTodosForCategoryResponse> response = await handler.HandleAsync(command);
        return response.IsSuccess && response.Value is not null && response.Value.TodosForCategory is IOrderedEnumerable<Todo> todos
            ? TypedResults.Ok(todos)
            : TypedResults.InternalServerError("An unknown error occurred when getting todos.");
    });
app.MapPut(pattern: "/category/{id:guid}",
    handler: async Task<Results<NoContent, NotFound<string>, InternalServerError<string>>>
    (Guid id, UpdateCategoryNameCommand command, ICommandHandler<UpdateCategoryNameCommand, UpdateCategoryNameResponse> handler) =>
    {
        Result<UpdateCategoryNameResponse> response = await handler.HandleAsync(command);
        return response.IsSuccess && response.Value is not null && response.Value.IsSuccess
            ? TypedResults.NoContent()
            : response.IsFailure && response.Error is not null
                ? TypedResults.NotFound(response.Error)
                : TypedResults.InternalServerError("An unknown error occurred when renaming the category.");
    });
app.MapDelete(pattern: "/category/{id:guid}",
    handler: async Task<Results<NoContent, NotFound<string>, InternalServerError<string>>>
    (Guid id, ICommandHandler<DeleteTodoCommand, DeleteTodoResponse> handler) =>
    {
        DeleteTodoCommand command = new(id);

        Result<DeleteTodoResponse> response = await handler.HandleAsync(command);
        return response.IsSuccess && response.Value is not null && response.Value.IsSuccess
            ? TypedResults.NoContent()
            : response.IsFailure && response.Error is not null
                ? TypedResults.NotFound(response.Error)
                : TypedResults.InternalServerError("An unknown error occurred when deleting the category.");
    });

app.MapPost(pattern: "/todo",
    handler: async Task<Results<BadRequest<string>, Created<Todo>, InternalServerError<string>>> (CreateTodoCommand command,
    ICommandHandler<CreateTodoCommand, CreateTodoResponse> handler) =>
    {
        Result<CreateTodoResponse> response = await handler.HandleAsync(command);
        return response.IsFailure && response.Error is not null
            ? TypedResults.BadRequest(response.Error)
            : response.IsSuccess && response.Value is not null && response.Value.Todo is Todo t
                ? TypedResults.Created("", t)
                : TypedResults.InternalServerError("An unknown error occurred when creating the todo.");
    });
app.MapGet(pattern: "/todo/complete",
    handler: async Task<Results<Ok<IOrderedEnumerable<Todo>>, InternalServerError<string>>>
    (ICommandHandler<GetTodosCompletedCommand, GetTodosCompletedResponse> handler) =>
    {
        GetTodosCompletedCommand command = new();

        Result<GetTodosCompletedResponse> response = await handler.HandleAsync(command);
        return response.IsSuccess && response.Value is not null && response.Value.CompleteTodos is IOrderedEnumerable<Todo> todos
            ? TypedResults.Ok(todos)
            : TypedResults.InternalServerError("An unknown error occurred when getting todos.");
    });
app.MapGet(pattern: "/todo/duetoday",
    handler: async Task<Results<Ok<IOrderedEnumerable<Todo>>, InternalServerError<string>>>
    (ICommandHandler<GetTodosDueTodayCommand, GetTodosDueTodayResponse> handler) =>
    {
        GetTodosDueTodayCommand command = new();

        Result<GetTodosDueTodayResponse> response = await handler.HandleAsync(command);
        return response.IsSuccess && response.Value is not null && response.Value.DueTodayTodos is IOrderedEnumerable<Todo> todos
            ? TypedResults.Ok(todos)
            : TypedResults.InternalServerError("An unknown error occurred when getting todos.");
    });
app.MapGet(pattern: "/todo/important",
    handler: async Task<Results<Ok<IOrderedEnumerable<Todo>>, InternalServerError<string>>>
    (ICommandHandler<GetTodosImportantCommand, GetTodosImportantResponse> handler) =>
    {
        GetTodosImportantCommand command = new();

        Result<GetTodosImportantResponse> response = await handler.HandleAsync(command);
        return response.IsSuccess && response.Value is not null && response.Value.ImportantTodos is IOrderedEnumerable<Todo> todos
            ? TypedResults.Ok(todos)
            : TypedResults.InternalServerError("An unknown error occurred when getting todos.");
    });
app.MapGet(pattern: "/todo/overdue",
    handler: async Task<Results<Ok<IOrderedEnumerable<Todo>>, InternalServerError<string>>>
    (ICommandHandler<GetTodosOverdueCommand, GetTodosOverdueResponse> handler) =>
    {
        GetTodosOverdueCommand command = new();

        Result<GetTodosOverdueResponse> response = await handler.HandleAsync(command);
        return response.IsSuccess && response.Value is not null && response.Value.OverdueTodos is IOrderedEnumerable<Todo> todos
            ? TypedResults.Ok(todos)
            : TypedResults.InternalServerError("An unknown error occurred when getting todos.");
    });
app.MapPut(pattern: "/todo/{id:guid}/category",
    handler: async Task<Results<NoContent, NotFound<string>, InternalServerError<string>>>
    (Guid id, UpdateTodoCategoryCommand command, ICommandHandler<UpdateTodoCategoryCommand, UpdateTodoCategoryResponse> handler) =>
    {
        Result<UpdateTodoCategoryResponse> response = await handler.HandleAsync(command);
        return response.IsSuccess && response.Value is not null && response.Value.IsSuccess
            ? TypedResults.NoContent()
            : response.IsFailure && response.Error is not null
                ? TypedResults.NotFound(response.Error)
                : TypedResults.InternalServerError("An unknown error occurred when updating the todo.");
    });
app.MapPut(pattern: "/todo/{id:guid}/completedate",
    handler: async Task<Results<NoContent, NotFound<string>, InternalServerError<string>>>
    (Guid id, UpdateTodoCompleteDateCommand command, ICommandHandler<UpdateTodoCompleteDateCommand, UpdateTodoCompleteDateResponse> handler) =>
    {
        Result<UpdateTodoCompleteDateResponse> response = await handler.HandleAsync(command);
        return response.IsSuccess && response.Value is not null && response.Value.IsSuccess
            ? TypedResults.NoContent()
            : response.IsFailure && response.Error is not null
                ? TypedResults.NotFound(response.Error)
                : TypedResults.InternalServerError("An unknown error occurred when updating the todo.");
    });
app.MapPut(pattern: "/todo/{id:guid}/description",
    handler: async Task<Results<NoContent, NotFound<string>, InternalServerError<string>>>
    (Guid id, UpdateTodoDescriptionCommand command, ICommandHandler<UpdateTodoDescriptionCommand, UpdateTodoDescriptionResponse> handler) =>
    {
        Result<UpdateTodoDescriptionResponse> response = await handler.HandleAsync(command);
        return response.IsSuccess && response.Value is not null && response.Value.IsSuccess
            ? TypedResults.NoContent()
            : response.IsFailure && response.Error is not null
                ? TypedResults.NotFound(response.Error)
                : TypedResults.InternalServerError("An unknown error occurred when updating the todo.");
    });
app.MapPut(pattern: "/todo/{id:guid}/duedate",
    handler: async Task<Results<NoContent, NotFound<string>, InternalServerError<string>>>
    (Guid id, UpdateTodoDueDateCommand command, ICommandHandler<UpdateTodoDueDateCommand, UpdateTodoDueDateResponse> handler) =>
    {
        Result<UpdateTodoDueDateResponse> response = await handler.HandleAsync(command);
        return response.IsSuccess && response.Value is not null && response.Value.IsSuccess
            ? TypedResults.NoContent()
            : response.IsFailure && response.Error is not null
                ? TypedResults.NotFound(response.Error)
                : TypedResults.InternalServerError("An unknown error occurred when updating the todo.");
    });
app.MapPut(pattern: "/todo/{id:guid}/importance",
    handler: async Task<Results<NoContent, NotFound<string>, InternalServerError<string>>>
    (Guid id, UpdateTodoImportanceCommand command, ICommandHandler<UpdateTodoImportanceCommand, UpdateTodoImportanceResponse> handler) =>
    {
        Result<UpdateTodoImportanceResponse> response = await handler.HandleAsync(command);
        return response.IsSuccess && response.Value is not null && response.Value.IsSuccess
            ? TypedResults.NoContent()
            : response.IsFailure && response.Error is not null
                ? TypedResults.NotFound(response.Error)
                : TypedResults.InternalServerError("An unknown error occurred when updating the todo.");
    });
app.MapDelete(pattern: "/todo/{id:guid}",
    handler: async Task<Results<NoContent, NotFound<string>, InternalServerError<string>>>
    (Guid id, ICommandHandler<DeleteCategoryCommand, DeleteCategoryResponse> handler) =>
    {
        DeleteCategoryCommand command = new(id);

        Result<DeleteCategoryResponse> response = await handler.HandleAsync(command);
        return response.IsSuccess && response.Value is not null && response.Value.IsSuccess
            ? TypedResults.NoContent()
            : response.IsFailure && response.Error is not null
                ? TypedResults.NotFound(response.Error)
                : TypedResults.InternalServerError("An unknown error occurred when deleting the todo.");
    });

app.Run();
