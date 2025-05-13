
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.Category.Delete;

internal record Command(Guid Id) : ICommand<Response>;
