
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.DeleteTodo;

public sealed record DeleteTodoCommand(Guid TodoId) : ICommand<DeleteTodoResponse>;
