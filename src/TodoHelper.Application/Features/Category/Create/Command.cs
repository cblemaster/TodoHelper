
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.Category.Create;

internal sealed record Command(string Name) : ICommand<Response>;
