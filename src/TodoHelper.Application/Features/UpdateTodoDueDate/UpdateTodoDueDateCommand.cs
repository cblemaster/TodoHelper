
using TodoHelper.Application.Interfaces;

namespace TodoHelper.Application.Features.UpdateTodoDueDate;

public sealed record UpdateTodoDueDateCommand(Guid TodoId, DateOnly? DueDate) : ICommand<UpdateTodoDueDateResponse>;
