
namespace TodoHelper.Domain.ValueObjects;

public sealed class CompleteDate
{
    public DateTimeOffset? Value { get; }

    private CompleteDate(DateTimeOffset? value) => Value = value;

    internal static CompleteDate CreateNew() => new(null);
    public static CompleteDate Create(DateTimeOffset? value) => new(value);
}
