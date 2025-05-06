
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.UpdateTodoDueDate;

internal sealed record UpdateTodoDueDateCommand(Guid TodoId, DateOnly? DueDate) : ICommand<UpdateTodoDueDateResponse>;
