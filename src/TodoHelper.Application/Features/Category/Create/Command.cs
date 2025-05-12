
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.Category.Create;

internal record Command(string Name) : ICommand<Response>;
