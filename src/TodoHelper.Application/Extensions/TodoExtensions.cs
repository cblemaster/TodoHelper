
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Domain.Entities;

namespace TodoHelper.Application.Extensions;

internal static class TodoExtensions
{
    internal static TodoDTO MapToDTO(this Todo todo) =>
        new(todo.Id.Value, todo.Category.Name.Value, todo.CategoryId.Value,
            todo.Description.Value, todo.DueDate.Value, todo.CompleteDate.Value, todo.CreateDate.Value, todo.UpdateDate.Value, todo.Importance.IsImportant);
}
