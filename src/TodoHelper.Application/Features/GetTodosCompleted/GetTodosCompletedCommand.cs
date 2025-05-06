
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Application.Interfaces;
using TodoHelper.Domain.Entities;

namespace TodoHelper.Application.Features.GetTodosCompleted;

internal sealed class GetTodosCompletedCommand : ICommand<GetTodosCompletedResponse>
{
    internal Func<Todo, bool> WherePredicate() => t => t.CompleteDate.Value is not null;
    internal Func<TodoDTO, DateOnly?> SortByDueDatePredicate() => d => d.DueDate;
    internal Func<TodoDTO, string> SortByDescriptionPredicate() => d => d.Description;
}
