
namespace TodoHelper.Application.DataTransferObjects;

internal sealed record TodoDTO(Guid Id, string CategoryName, Guid CategoryId, string Description, DateOnly? DueDate,
    DateTimeOffset? CompleteDate, DateTimeOffset CreateDate, DateTimeOffset? UpdateDate, bool IsImportant);
