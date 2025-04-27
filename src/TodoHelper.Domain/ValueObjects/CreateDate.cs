
namespace TodoHelper.Domain.ValueObjects;

public sealed class CreateDate
{
    public DateTimeOffset Value { get; }

    private CreateDate(DateTimeOffset value) => Value = value;

    internal static CreateDate CreateNew() => new();
}
