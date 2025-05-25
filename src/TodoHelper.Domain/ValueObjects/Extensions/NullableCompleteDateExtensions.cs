
namespace TodoHelper.Domain.ValueObjects.Extensions;

public static class NullableCompleteDateExtensions
{
    public static DateTimeOffset? ToNullableDateTimeOffset(this CompleteDate? completeDate) =>
        completeDate is null || completeDate.Value.DateValue is null
            ? null
            : completeDate.Value.DateValue;
}
