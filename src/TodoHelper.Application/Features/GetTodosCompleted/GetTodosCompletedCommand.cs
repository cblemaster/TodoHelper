
using System.Linq.Expressions;
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.GetTodosCompleted;

internal sealed class GetTodosCompletedCommand : ICommand<GetTodosCompletedResponse>
{
    internal Expression<Func<TodoDTO, DateTimeOffset?>> FirstOrderByPredicate { get; } = t => t.CompleteDate;
    internal Expression<Func<TodoDTO, string>> SecondOrderByPredicate { get; } = t => t.Description;
}
