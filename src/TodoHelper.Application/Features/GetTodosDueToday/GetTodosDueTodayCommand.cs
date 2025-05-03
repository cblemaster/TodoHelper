
using System.Linq.Expressions;
using TodoHelper.Application.Interfaces;
using TodoHelper.Domain.Entities;

namespace TodoHelper.Application.Features.GetTodosDueToday;

public class GetTodosDueTodayCommand : ICommand<GetTodosDueTodayResponse>
{
    public Expression<Func<Todo, string>> OrderByPredicate { get; } = t => t.Description.Value;
}
