
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Application.Features.Common.Specifications;
using TodoHelper.Application.Interfaces;
using TodoHelper.Domain.Entities;

namespace TodoHelper.Application.Features.GetTodosImportant;

internal sealed class GetTodosImportantCommand : ICommand<GetTodosImportantResponse>
{
    internal Func<Todo, bool> WherePredicate() => t => t.Importance.IsImportant;
    internal Func<TodoDTO, string> SortByDescriptionPredicate() => Predicates.SortByDescriptionPredicate();
    internal Func<TodoDTO, DateOnly?> SortByDueDatePredicate() => Predicates.SortByDueDatePredicate();
}
