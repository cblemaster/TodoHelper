
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.UpdateTodoDescription;

public sealed class UpdateTodoDescriptionCommand(Guid todoId, string name) : ICommand<UpdateTodoDescriptionResponse>
{
    public Guid TodoId { get; } = todoId;
    public string Name { get; } = name;
}
