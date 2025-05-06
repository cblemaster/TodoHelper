
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Domain.Entities;

namespace TodoHelper.Application.Extensions;

internal static class TodoExtensions
{
    internal static TodoDTO MapToDTO(this Todo entity) =>
        new(entity.Id.Value, entity.Category.Name.Value, entity.CategoryId.Value,
            entity.Description.Value, entity.DueDate.Value, entity.CompleteDate.Value, entity.CreateDate.Value, entity.UpdateDate.Value, entity.Importance.IsImportant);
}
