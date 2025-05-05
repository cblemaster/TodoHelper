
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.UpdateTodoDescription;

public sealed record UpdateTodoDescriptionCommand(Guid TodoId, string Description) : ICommand<UpdateTodoDescriptionResponse>;
