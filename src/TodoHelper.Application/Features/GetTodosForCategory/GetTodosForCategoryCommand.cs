
using System.Linq.Expressions;
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.GetTodosForCategory;

internal sealed class GetTodosForCategoryCommand(Guid categoryId) : ICommand<GetTodosForCategoryResponse>
{
    internal Guid CategoryId { get; } = categoryId;

    internal Expression<Func<TodoDTO, DateOnly?>> FirstOrderByPredicate { get; } = t => t.DueDate;
    internal Expression<Func<TodoDTO, string>> SecondOrderByPredicate { get; } = t => t.Description;
}
