
namespace TodoHelper.Application.DataTransferObjects;

public sealed record CategoryDTO(Guid Id, string Name, int CountOfTodosNotComplete, DateTimeOffset CreateDate, DateTimeOffset? UpdateDate);
