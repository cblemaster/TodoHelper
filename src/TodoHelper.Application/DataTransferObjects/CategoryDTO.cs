
namespace TodoHelper.Application.DataTransferObjects;

internal sealed record CategoryDTO(Guid Id, string Name, int CountOfTodosNotComplete);
