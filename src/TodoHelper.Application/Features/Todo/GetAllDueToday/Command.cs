
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.Todo.GetAllDueToday;

internal sealed record Command(bool IncludeComplete) : ICommand<Response>;
