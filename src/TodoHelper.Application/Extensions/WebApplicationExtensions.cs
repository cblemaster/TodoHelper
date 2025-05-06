
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

namespace TodoHelper.Application.Extensions;

internal static class WebApplicationExtensions
{
    internal static WebApplication MapEndpoints(this WebApplication app)
    {
#pragma warning disable IDE0058
        app.MapGet("/", () => "Welcome!");
        app.MapCreateCategoryEndpoint()
            .MapCreateTodoEndpoint()
            .MapDeleteCategoryEndpoint()
            .MapDeleteTodoEndpoint()
            .MapGetCategoriesEndpoint()
            .MapGetTodosCompletedEndpoint()
            .MapGetTodosDueTodayEndpoint()
            .MapGetTodosForCategoryEndpoint()
            .MapGetTodosImportantEndpoint()
            .MapGetTodosOverdueEndpoint()
            .MapUpdateCategoryNameEndpoint()
            .MapUpdateTodoCategoryEndpoint()
            .MapUpdateTodoCompleteDateEndpoint()
            .MapUpdateTodoDescriptionEndpoint()
            .MapUpdateTodoDueDateEndpoint()
            .MapUpdateTodoImportanceEndpoint();
#pragma warning restore IDE0058
        return app;
    }
}
