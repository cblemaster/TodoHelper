
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Entities;
using CreateCategory = TodoHelper.Application.Features.Category.Create;
using CreateTodo = TodoHelper.Application.Features.Todo.Create;
using DeleteCategory = TodoHelper.Application.Features.Category.Delete;
using DeleteTodo = TodoHelper.Application.Features.Todo.Delete;
using GetCategories = TodoHelper.Application.Features.Category.GetAll;
using GetCategory = TodoHelper.Application.Features.Category.Get;
using GetTodo = TodoHelper.Application.Features.Todo.Get;
using GetTodos = TodoHelper.Application.Features.Todo.GetAll;
using UpdateCategory = TodoHelper.Application.Features.Category.Update;
using UpdateTodo = TodoHelper.Application.Features.Todo.Update;
using UpdateTodoCompleteDate = TodoHelper.Application.Features.Todo.UpdateCompleteDate;
using GetTodosByCategory = TodoHelper.Application.Features.Todo.GetByCategory;
using GetTodosCompleted = TodoHelper.Application.Features.Todo.GetCompleted;
using GetTodosDueToday = TodoHelper.Application.Features.Todo.GetDueToday;
using GetTodosImportant = TodoHelper.Application.Features.Todo.GetImportant;
using GetTodosOverdue = TodoHelper.Application.Features.Todo.GetOverdue;


namespace TodoHelper.Application.Extensions;

internal static class WebApplicationBuilderExtensions
{
    internal static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
    {
        _ = builder.Services
            
            .AddScoped<IRepository<Category>, TodoRepository<Category>>()
            .AddScoped<IRepository<Todo>, TodoRepository<Todo>>()

            .AddScoped<CreateCategory.Handler>()
            .AddScoped<CreateTodo.Handler>()
            .AddScoped<GetCategories.Handler>()
            .AddScoped<GetCategory.Handler>()
            .AddScoped<GetTodos.Handler>()
            .AddScoped<GetTodo.Handler>()
            .AddScoped<GetTodosByCategory.Handler>()
            .AddScoped<GetTodosCompleted.Handler>()
            .AddScoped<GetTodosDueToday.Handler>()
            .AddScoped<GetTodosImportant.Handler>()
            .AddScoped<GetTodosOverdue.Handler>()
            .AddScoped<UpdateCategory.Handler>()
            .AddScoped<UpdateTodo.Handler>()
            .AddScoped<UpdateTodoCompleteDate.Handler>()
            .AddScoped<DeleteCategory.Handler>()
            .AddScoped<DeleteTodo.Handler>()
            ;
        
        return builder;
    }
}
