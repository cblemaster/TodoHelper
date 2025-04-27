
namespace TodoHelper.Domain.ValueObjects;

public sealed class UpdateDate
{
    public DateTimeOffset? Value { get; }

    private UpdateDate() => Value = DateTimeOffset.UtcNow;

    internal static UpdateDate CreateNew() => new();
}
