
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.UpdateTodoCategory;

internal sealed record UpdateTodoCategoryCommand(Guid TodoId, Guid CategoryId) : ICommand<UpdateTodoCategoryResponse>;
