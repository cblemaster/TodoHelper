
using System.Linq.Expressions;
using TodoHelper.Application.Interfaces;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.ValueObjects;

namespace TodoHelper.Application.Features.SeeTodosForCategory;

public class GetTodosForCategoryCommand(Identifier<Category> categoryId) : ICommand<GetTodosForCategoryResponse>
{
    public Identifier<Category> CategoryId { get; } = categoryId;

    public Expression<Func<Todo, bool>> FirstOrderByPredicate { get; } = t => t.IsComplete;
    public Expression<Func<Todo, DateOnly?>> SecondOrderByPredicate { get; } = t => t.DueDate.Value;
    public Expression<Func<Todo, string>> ThirdOrderByPredicate { get; } = t => t.Description.Value;
}
