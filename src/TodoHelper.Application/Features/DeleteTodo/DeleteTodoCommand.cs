
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.DeleteTodo;

internal sealed record DeleteTodoCommand(Guid TodoId) : ICommand<DeleteTodoResponse>;
