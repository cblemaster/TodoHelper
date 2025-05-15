
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.Category.Get;

internal sealed record Command(Guid Id) : ICommand<Response>;
