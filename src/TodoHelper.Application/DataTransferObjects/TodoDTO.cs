
namespace TodoHelper.Application.DataTransferObjects;

internal sealed record TodoDTO(Guid Id, string CategoryName, Guid CategoryId, string Description,
    DateOnly? DueDate, DateTimeOffset? CompleteDate, bool IsImportant);
