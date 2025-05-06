
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.UpdateTodoImportance;

internal sealed record UpdateTodoImportanceCommand(Guid TodoId) : ICommand<UpdateTodoImportanceResponse>;
