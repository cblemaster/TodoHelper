
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.UpdateTodoDueDate;

public sealed class UpdateTodoDueDateCommand(Guid todoId, DateOnly? dueDate) : ICommand<UpdateTodoDueDateResponse>
{
    public Guid TodoId { get; } = todoId;
    public DateOnly? DueDate { get; } = dueDate;
}
