
using System.Linq.Expressions;
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.GetTodosOverdue;

internal sealed class GetTodosOverdueCommand : ICommand<GetTodosOverdueResponse>
{
    internal Expression<Func<TodoDTO, DateOnly?>> FirstOrderByPredicate { get; } = t => t.DueDate;
    internal Expression<Func<TodoDTO, string>> SecondOrderByPredicate { get; } = t => t.Description;
}

