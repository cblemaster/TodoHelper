
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.UpdateTodoCategory;

public sealed class UpdateTodoCategoryCommand(Guid TodoId, Guid CategoryId) : ICommand<UpdateTodoCategoryResponse>
{
    public Guid TodoId { get; } = TodoId;
    public Guid CategoryId { get; } = CategoryId;
}
