
using System.Linq.Expressions;
using TodoHelper.Application.Interfaces;
using TodoHelper.Domain.Entities;

namespace TodoHelper.Application.Features.GetTodosDueToday;

internal sealed class GetTodosDueTodayCommand : ICommand<GetTodosDueTodayResponse>
{
    internal Expression<Func<Todo, string>> OrderByPredicate { get; } = t => t.Description.Value;
}
