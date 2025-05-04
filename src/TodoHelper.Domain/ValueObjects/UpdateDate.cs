
namespace TodoHelper.Domain.ValueObjects;

public sealed class UpdateDate
{
    public DateTimeOffset? Value { get; }

    private UpdateDate(DateTimeOffset? value) => Value = value;

    internal static UpdateDate CreateNew() => new(null);
    public static UpdateDate Create() => new(DateTimeOffset.UtcNow);
    public static UpdateDate Create(DateTimeOffset? value) => new(value);
}
