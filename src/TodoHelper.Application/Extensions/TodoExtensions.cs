
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.ValueObjects.Extensions;

namespace TodoHelper.Application.Extensions;

internal static class TodoExtensions
{
    internal static TodoDTO MapToDTO(this Todo todo) =>
        new(todo.Id.Value, todo.Category.Name.Value, todo.CategoryId.Value, todo.Description.Value,
            todo.DueDate.MapToNullableDateOnly(), todo.CompleteDate.MapToNullableDateTimeOffset(), todo.Importance.IsImportant);
}
