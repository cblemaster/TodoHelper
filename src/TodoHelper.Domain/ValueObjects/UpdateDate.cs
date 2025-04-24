
namespace TodoHelper.Domain.ValueObjects;

internal sealed class UpdateDate
{
    internal DateTimeOffset? Value { get; }

    private UpdateDate() => Value = DateTimeOffset.UtcNow;

    internal static UpdateDate CreateNew() => new();
}
