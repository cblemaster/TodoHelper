
using System.Linq.Expressions;
using TodoHelper.Application.Interfaces;
using TodoHelper.Domain.Entities;

namespace TodoHelper.Application.Features.GetTodosImportant;

internal sealed class GetTodosImportantCommand : ICommand<GetTodosImportantResponse>
{
    internal Expression<Func<Todo, DateOnly?>> FirstOrderByPredicate { get; } = t => t.DueDate.Value;
    internal Expression<Func<Todo, string>> SecondOrderByPredicate { get; } = t => t.Description.Value;
}
