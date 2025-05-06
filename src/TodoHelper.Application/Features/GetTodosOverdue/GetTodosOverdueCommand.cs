
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Application.Interfaces;
using TodoHelper.Domain.Entities;

namespace TodoHelper.Application.Features.GetTodosOverdue;

internal sealed class GetTodosOverdueCommand : ICommand<GetTodosOverdueResponse>
{
    internal Func<TodoDTO, string> SortByDescriptionPredicate() => d => d.Description;
    internal Func<TodoDTO, DateOnly?> SortByDueDatePredicate() => d => d.DueDate;
    internal Func<Todo, bool> WherePredicate() => t => t.DueDate.Value is not null && t.DueDate.Value < DateOnly.FromDateTime(DateTime.Today);
}

