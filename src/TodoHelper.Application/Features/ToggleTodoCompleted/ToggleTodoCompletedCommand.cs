
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.ToggleTodoCompleted;

public sealed class ToggleTodoCompletedCommand(Guid todoId, DateTimeOffset? completeDate) : ICommand<ToggleTodoCompletedResponse>
{
    public Guid TodoId { get; } = todoId;
    public DateTimeOffset? CompleteDate { get; } = completeDate;
}
