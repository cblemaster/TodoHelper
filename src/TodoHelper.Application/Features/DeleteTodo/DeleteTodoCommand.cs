
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.DeleteTodo;

public sealed class DeleteTodoCommand(Guid todoId) : ICommand<DeleteTodoResponse>
{
    public Guid TodoId { get; } = todoId;
}
