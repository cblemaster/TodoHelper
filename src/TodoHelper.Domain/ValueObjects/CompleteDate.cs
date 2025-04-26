
namespace TodoHelper.Domain.ValueObjects;

internal sealed class CompleteDate
{
    internal DateTimeOffset? Value { get; }

    private CompleteDate(DateTimeOffset? value) => Value = value;

    internal static CompleteDate CreateNew(DateTimeOffset? value) => new(value);
}
