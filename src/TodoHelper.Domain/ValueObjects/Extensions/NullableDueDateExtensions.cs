
namespace TodoHelper.Domain.ValueObjects.Extensions;

public static class NullableDueDateExtensions
{
    public static DateOnly? MapToNullableDateOnly(this DueDate? dueDate) =>
        dueDate is null || dueDate.Value.Value is null
            ? null
            : dueDate.Value.Value;
}
