
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.UpdateTodoCategory;

public sealed record UpdateTodoCategoryCommand(Guid TodoId, Guid CategoryId) : ICommand<UpdateTodoCategoryResponse>;
