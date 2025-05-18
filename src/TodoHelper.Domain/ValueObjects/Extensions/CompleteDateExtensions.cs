
namespace TodoHelper.Domain.ValueObjects.Extensions;

public static class CompleteDateExtensions
{
    public static DateTimeOffset? MapToNullableDateTimeOffset(this CompleteDate? completeDate) =>
        completeDate is null || completeDate.Value.Value is null
            ? null
            : completeDate.Value.Value;
}
