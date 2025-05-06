
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Application.Features.Common.Specifications;
using TodoHelper.Application.Interfaces;
using TodoHelper.Domain.Entities;

namespace TodoHelper.Application.Features.GetTodosForCategory;

internal sealed class GetTodosForCategoryCommand(Guid categoryId) : ICommand<GetTodosForCategoryResponse>
{
    internal Guid CategoryId { get; } = categoryId;

    internal Func<Todo, bool> WherePredicate(Guid id) => t => t.CategoryId.Value == id;
    internal Func<TodoDTO, string> SortByDescriptionPredicate() => Predicates.SortByDescriptionPredicate();
    internal Func<TodoDTO, DateOnly?> SortByDueDatePredicate() => Predicates.SortByDueDatePredicate();
}
