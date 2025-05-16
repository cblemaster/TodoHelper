
using TodoHelper.Application.Features.Category.Create;
using TodoHelper.Application.Features.Category.Delete;
using TodoHelper.Application.Features.Category.Get;
using TodoHelper.Application.Features.Category.GetAll;
using TodoHelper.Application.Features.Category.Update;
using TodoHelper.Application.Features.Todo.Create;
using TodoHelper.Application.Features.Todo.Delete;
using TodoHelper.Application.Features.Todo.Get;
using TodoHelper.Application.Features.Todo.GetAll;
using TodoHelper.Application.Features.Todo.Update;

namespace TodoHelper.Application.Extensions;

internal static class WebApplicationExtensions
{
    internal static WebApplication MapEndpoints(this WebApplication app)
    {
        _ = app.MapCreateCategory()
            .MapCreateTodo()
            .MapGetAllCategory()
            .MapGetAllTodo()
            .MapGetCategory()
            .MapGetTodo()
            .MapDeleteCategory()
            .MapDeleteTodo()
            .MapUpdateCategory()
            .MapUpdateTodo();
        return app;
    }
}
