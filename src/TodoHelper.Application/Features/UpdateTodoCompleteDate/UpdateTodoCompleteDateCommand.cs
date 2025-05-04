
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.ToggleTodoCompleted;

public sealed class UpdateTodoCompleteDateCommand(Guid todoId, DateTimeOffset? completeDate) : ICommand<UpdateTodoCompleteDateResponse>
{
    public Guid TodoId { get; } = todoId;
    public DateTimeOffset? CompleteDate { get; } = completeDate;
}
