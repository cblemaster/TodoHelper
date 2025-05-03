
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.CreateTodo;

internal sealed record CreateTodoCommand(Guid CategoryId, string Description, DateOnly? DueDate, bool IsImportant) : ICommand<CreateTodoResponse>;
