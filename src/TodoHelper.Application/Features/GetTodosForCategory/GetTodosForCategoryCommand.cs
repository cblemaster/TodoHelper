
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Application.Interfaces;
using TodoHelper.Domain.Entities;

namespace TodoHelper.Application.Features.GetTodosForCategory;

internal sealed class GetTodosForCategoryCommand(Guid categoryId) : ICommand<GetTodosForCategoryResponse>
{
    internal Guid CategoryId { get; } = categoryId;
    internal Func<TodoDTO, DateOnly?> SortByDueDatePredicate() => d => d.DueDate;
    internal Func<TodoDTO, string> SortByDescriptionPredicate() => d => d.Description;
    internal Func<Todo, bool> WherePredicate(GetTodosForCategoryCommand command) => t => t.CategoryId.Value == command.CategoryId;
}
