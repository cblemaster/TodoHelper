
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.Category.Update;

internal sealed record Command(Guid Id, string Name) : ICommand<Response>;
