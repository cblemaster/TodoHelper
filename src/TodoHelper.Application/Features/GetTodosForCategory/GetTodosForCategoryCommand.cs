
using System.Linq.Expressions;
using TodoHelper.Application.Interfaces;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.ValueObjects;

namespace TodoHelper.Application.Features.SeeTodosForCategory;

internal sealed class GetTodosForCategoryCommand(Identifier<Category> categoryId) : ICommand<GetTodosForCategoryResponse>
{
    internal Identifier<Category> CategoryId { get; } = categoryId;

    internal Expression<Func<Todo, bool>> FirstOrderByPredicate { get; } = t => t.IsComplete;
    internal Expression<Func<Todo, DateOnly?>> SecondOrderByPredicate { get; } = t => t.DueDate.Value;
    internal Expression<Func<Todo, string>> ThirdOrderByPredicate { get; } = t => t.Description.Value;
}
