
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

namespace TodoHelper.Application.Extensions;

internal static class WebApplicationBuilderExtensions
{
    internal static WebApplicationBuilder RegisterHandlers(this WebApplicationBuilder builder)
    {
        _ = builder.Services
            .AddScoped<ICommandHandler<CreateCategoryCommand, CreateCategoryResponse>, CreateCategoryHandler>()
            .AddScoped<ICommandHandler<CreateTodoCommand, CreateTodoResponse>, CreateTodoHandler>()
            .AddScoped<ICommandHandler<DeleteCategoryCommand, DeleteCategoryResponse>, DeleteCategoryHandler>()
            .AddScoped<ICommandHandler<DeleteTodoCommand, DeleteTodoResponse>, DeleteTodoHandler>()
            .AddScoped<ICommandHandler<GetCategoriesCommand, GetCategoriesResponse>, GetCategoriesHandler>()
            .AddScoped<ICommandHandler<GetTodosCompletedCommand, GetTodosCompletedResponse>, GetTodosCompletedHandler>()
            .AddScoped<ICommandHandler<GetTodosDueTodayCommand, GetTodosDueTodayResponse>, GetTodosDueTodayHandler>()
            .AddScoped<ICommandHandler<GetTodosForCategoryCommand, GetTodosForCategoryResponse>, GetTodosForCategoryHandler>()
            .AddScoped<ICommandHandler<GetTodosImportantCommand, GetTodosImportantResponse>, GetTodosImportantHandler>()
            .AddScoped<ICommandHandler<GetTodosOverdueCommand, GetTodosOverdueResponse>, GetTodosOverdueHandler>()
            .AddScoped<ICommandHandler<UpdateCategoryNameCommand, UpdateCategoryNameResponse>, UpdateCategoryNameHandler>()
            .AddScoped<ICommandHandler<UpdateTodoCompleteDateCommand, UpdateTodoCompleteDateResponse>, UpdateTodoCompleteDateHandler>()
            .AddScoped<ICommandHandler<UpdateTodoImportanceCommand, UpdateTodoImportanceResponse>, UpdateTodoImportanceHandler>()
            .AddScoped<ICommandHandler<UpdateTodoCategoryCommand, UpdateTodoCategoryResponse>, UpdateTodoCategoryHandler>()
            .AddScoped<ICommandHandler<UpdateTodoDescriptionCommand, UpdateTodoDescriptionResponse>, UpdateTodoDescriptionHandler>()
            .AddScoped<ICommandHandler<UpdateTodoDueDateCommand, UpdateTodoDueDateResponse>, UpdateTodoDueDateHandler>();

        return builder;
    }
}
