
using System.Linq.Expressions;
using TodoHelper.Application.Interfaces;
using TodoHelper.Domain.Entities;

namespace TodoHelper.Application.Features.GetTodosImportant;

public class GetTodosImportantCommand : ICommand<GetTodosImportantResponse>
{
    public Expression<Func<Todo, DateOnly?>> FirstOrderByPredicate { get; } = t => t.DueDate.Value;
    public Expression<Func<Todo, string>> SecondOrderByPredicate { get; } = t => t.Description.Value;
}
