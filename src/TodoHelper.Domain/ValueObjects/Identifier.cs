
namespace TodoHelper.Domain.ValueObjects;

internal sealed class Identifier<T>
{
    internal Guid Value { get; }

    private Identifier() => Value = Guid.NewGuid();

    internal static Identifier<T> CreateNew() => new();

    public override bool Equals(object? obj) => obj is Identifier<T> other && Value == other.Value;

    public override int GetHashCode() => HashCode.Combine(Value);

    public static bool operator ==(Identifier<T> left, Identifier<T> right) =>
        left is null ? right is null : right is not null && left.Equals(right);

    public static bool operator !=(Identifier<T> left, Identifier<T> right) => !(left == right);
}
