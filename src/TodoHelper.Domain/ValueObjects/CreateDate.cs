
namespace TodoHelper.Domain.ValueObjects;

internal sealed class CreateDate
{
    internal DateTimeOffset? Value { get; }

    private CreateDate() => Value = DateTimeOffset.UtcNow;

    internal static CreateDate CreateNew() => new();
}
