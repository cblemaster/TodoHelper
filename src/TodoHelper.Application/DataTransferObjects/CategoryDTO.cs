
namespace TodoHelper.Application.DataTransferObjects;

internal sealed record CategoryDTO(Guid Id, string Name, uint CountOfTodosNotComplete);
