
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Application.Features.Common;
using TodoHelper.Application.Interfaces;
using TodoHelper.Domain.Entities;

namespace TodoHelper.Application.Features.GetTodosDueToday;

internal sealed class GetTodosDueTodayCommand : ICommand<GetTodosDueTodayResponse>
{
    internal Func<Todo, bool> WherePredicate() => t => t.DueDate.Value is not null && t.DueDate.Value == DateOnly.FromDateTime(DateTime.Today);
    internal Func<TodoDTO, string> SortByDescriptionPredicate() => Predicates.SortByDescriptionPredicate();
}
