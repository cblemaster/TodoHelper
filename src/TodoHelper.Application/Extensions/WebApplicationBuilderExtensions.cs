
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

namespace TodoHelper.Application.Extensions;

internal static class WebApplicationBuilderExtensions
{
    internal static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
    {
        _ = builder.Services
            .AddScoped<IRepository<Category>, Repository<Category>>()
            .AddScoped<IRepository<Todo>, Repository<Todo>>()
            .AddScoped<CreateCategory.Handler>()
            .AddScoped<GetCategories.Handler>()
            .AddScoped<GetCategory.Handler>()
            .AddScoped<UpdateCategory.Handler>()
            .AddScoped<DeleteCategory.Handler>()
            .AddScoped<CreateTodo.Handler>()
            .AddScoped<GetTodos.Handler>()
            .AddScoped<GetTodo.Handler>()
            .AddScoped<UpdateTodo.Handler>()
            .AddScoped<DeleteTodo.Handler>();
        return builder;
    }
}
