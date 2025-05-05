
using System.Linq.Expressions;
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.GetTodosDueToday;

internal sealed class GetTodosDueTodayCommand : ICommand<GetTodosDueTodayResponse>
{
    internal Expression<Func<TodoDTO, string>> OrderByPredicate { get; } = t => t.Description;
}
