
using TodoHelper.Application.Features.Category.Create;
using TodoHelper.Application.Features.Category.Delete;
using TodoHelper.Application.Features.Category.Get;
using TodoHelper.Application.Features.Category.GetAll;
using TodoHelper.Application.Features.Category.Update;
using TodoHelper.Application.Features.Todo.Create;
using TodoHelper.Application.Features.Todo.Delete;
using TodoHelper.Application.Features.Todo.Get;
using TodoHelper.Application.Features.Todo.GetAll;
using TodoHelper.Application.Features.Todo.GetByCategory;
using TodoHelper.Application.Features.Todo.GetCompleted;
using TodoHelper.Application.Features.Todo.GetDueToday;
using TodoHelper.Application.Features.Todo.GetImportant;
using TodoHelper.Application.Features.Todo.GetOverdue;
using TodoHelper.Application.Features.Todo.Update;
using TodoHelper.Application.Features.Todo.UpdateCompleteDate;

namespace TodoHelper.Application.Extensions;

internal static class WebApplicationExtensions
{
    internal static WebApplication MapEndpoints(this WebApplication app)
    {
        _ = app.MapCreateCategory()
            .MapCreateTodo()
            .MapGetCategories()
            .MapGetTodos()
            .MapGetCategory()
            .MapGetTodo()
            .MapGetTodosByCategory()
            .MapGetTodosCompleted()
            .MapGetTodosDueToday()
            .MapGetTodosImportant()
            .MapGetTodosOverdue()
            .MapDeleteCategory()
            .MapDeleteTodo()
            .MapUpdateCategory()
            .MapUpdateTodo()
            .MapUpdateTodoCompleteDate()
            ;
        return app;
    }
}
