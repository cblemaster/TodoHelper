
namespace TodoHelper.Domain.ValueObjects;

internal sealed class Identifier<T>
{
    internal Guid Value { get; }

    private Identifier() => Value = Guid.NewGuid();

    internal static Identifier<T> CreateNew() => new();
}
