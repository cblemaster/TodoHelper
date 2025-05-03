
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.ToggleTodoImportance;

public sealed class ToggleTodoImportanceCommand(Guid todoId) : ICommand<ToggleTodoImportanceResponse>
{
    public Guid TodoId { get; } = todoId;
}
