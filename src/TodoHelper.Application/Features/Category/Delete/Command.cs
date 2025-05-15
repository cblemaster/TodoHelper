
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.Category.Delete;

internal sealed record Command(Guid Id) : ICommand<Response>;
