
using TodoHelper.Domain.ValueObjects;

namespace TodoHelper.Domain.Extensions;

public static class DueDateExtensions
{
    public static DateOnly? ToNullableDateOnly(this DueDate? dueDate) =>
        dueDate is not null && dueDate.Value.Value is not null
            ? dueDate.Value.Value
            : null;
}
