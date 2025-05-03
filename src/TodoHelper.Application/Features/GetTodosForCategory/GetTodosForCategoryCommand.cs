
using System.Linq.Expressions;
using TodoHelper.Application.Interfaces;
using TodoHelper.Domain.Entities;

namespace TodoHelper.Application.Features.GetTodosForCategory;

internal sealed class GetTodosForCategoryCommand(Guid categoryId) : ICommand<GetTodosForCategoryResponse>
{
    internal Guid CategoryId { get; } = categoryId;

    internal Expression<Func<Todo, bool>> FirstOrderByPredicate { get; } = t => t.IsComplete;
    internal Expression<Func<Todo, DateOnly?>> SecondOrderByPredicate { get; } = t => t.DueDate.Value;
    internal Expression<Func<Todo, string>> ThirdOrderByPredicate { get; } = t => t.Description.Value;
}
