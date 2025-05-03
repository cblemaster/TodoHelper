
using System.Linq.Expressions;
using TodoHelper.Application.Interfaces;
using TodoHelper.Domain.Entities;

namespace TodoHelper.Application.Features.GetTodosCompleted;

public class GetTodosCompletedCommand : ICommand<GetTodosCompletedResponse>
{
    public Expression<Func<Todo, DateTimeOffset?>> FirstOrderByPredicate { get; } = t => t.CompleteDate.Value;
    public Expression<Func<Todo, string>> SecondOrderByPredicate { get; } = t => t.Description.Value;
}
