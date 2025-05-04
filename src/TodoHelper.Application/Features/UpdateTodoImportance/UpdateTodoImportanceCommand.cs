
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.ToggleTodoImportance;

public sealed record UpdateTodoImportanceCommand(Guid TodoId) : ICommand<UpdateTodoImportanceResponse>;
