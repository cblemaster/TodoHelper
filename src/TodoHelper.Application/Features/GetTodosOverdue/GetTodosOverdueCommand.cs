
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Application.Features.Common.Specifications;
using TodoHelper.Application.Interfaces;
using TodoHelper.Domain.Entities;

namespace TodoHelper.Application.Features.GetTodosOverdue;

internal sealed class GetTodosOverdueCommand : ICommand<GetTodosOverdueResponse>
{
    internal Func<Todo, bool> WherePredicate() => t => t.DueDate.Value is not null && t.DueDate.Value < DateOnly.FromDateTime(DateTime.Today);
    internal Func<TodoDTO, string> SortByDescriptionPredicate() => Predicates.SortByDescriptionPredicate();
    internal Func<TodoDTO, DateOnly?> SortByDueDatePredicate() => Predicates.SortByDueDatePredicate();
}
