
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.Todo.GetDueToday;

internal sealed record Command(bool IncludeComplete) : ICommand<Response>;
