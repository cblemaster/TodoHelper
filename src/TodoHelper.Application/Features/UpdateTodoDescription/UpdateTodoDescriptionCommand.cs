
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.UpdateTodoDescription;

internal sealed record UpdateTodoDescriptionCommand(Guid TodoId, string Description) : ICommand<UpdateTodoDescriptionResponse>;
