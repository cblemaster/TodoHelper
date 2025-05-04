
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.ToggleTodoImportance;

public sealed class UpdateTodoImportanceCommand(Guid todoId) : ICommand<UpdateTodoImportanceResponse>
{
    public Guid TodoId { get; } = todoId;
}
