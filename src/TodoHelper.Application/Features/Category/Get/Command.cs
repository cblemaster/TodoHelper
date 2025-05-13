
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.Category.Get;

internal record Command(Guid Id) : ICommand<Response>;
