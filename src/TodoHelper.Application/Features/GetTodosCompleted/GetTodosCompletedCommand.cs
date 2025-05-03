
using System.Linq.Expressions;
using TodoHelper.Application.Interfaces;
using TodoHelper.Domain.Entities;

namespace TodoHelper.Application.Features.GetTodosCompleted;

internal sealed class GetTodosCompletedCommand : ICommand<GetTodosCompletedResponse>
{
    internal Expression<Func<Todo, DateTimeOffset?>> FirstOrderByPredicate { get; } = t => t.CompleteDate.Value;
    internal Expression<Func<Todo, string>> SecondOrderByPredicate { get; } = t => t.Description.Value;
}
