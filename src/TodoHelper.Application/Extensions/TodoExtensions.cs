
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.ValueObjects.Extensions;

namespace TodoHelper.Application.Extensions;

internal static class TodoExtensions
{
    internal static TodoDTO MapToDTO(this Todo todo) =>
        new(todo.Id.GuidValue, todo.Category.Name.StringValue, todo.CategoryId.GuidValue, todo.Description.StringValue,
            todo.DueDate.ToNullableDateOnly(), todo.CompleteDate.ToNullableDateTimeOffset(), todo.Importance.BoolValue);
}
