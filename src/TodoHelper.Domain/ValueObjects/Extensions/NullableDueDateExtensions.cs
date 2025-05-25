
namespace TodoHelper.Domain.ValueObjects.Extensions;

public static class NullableDueDateExtensions
{
    public static DateOnly? ToNullableDateOnly(this DueDate? dueDate) =>
        dueDate is null || dueDate.Value.DateValue is null
            ? null
            : dueDate.Value.DateValue;
}
