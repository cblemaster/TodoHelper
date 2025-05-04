
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.UpdateTodoImportance;

public sealed record UpdateTodoImportanceCommand(Guid TodoId) : ICommand<UpdateTodoImportanceResponse>;
