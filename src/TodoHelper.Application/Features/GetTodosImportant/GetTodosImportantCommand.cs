
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Application.Interfaces;
using TodoHelper.Domain.Entities;

namespace TodoHelper.Application.Features.GetTodosImportant;

internal sealed class GetTodosImportantCommand : ICommand<GetTodosImportantResponse>
{
    internal Func<TodoDTO, string> SortByDescriptionPredicate() => d => d.Description;
    internal Func<TodoDTO, DateOnly?> SortByDueDatePredicate() => d => d.DueDate;
    internal Func<Todo, bool> WherePredicate() => t => t.Importance.IsImportant;
}
