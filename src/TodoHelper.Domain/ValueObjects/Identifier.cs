
namespace TodoHelper.Domain.ValueObjects;

public sealed class Identifier<T>
{
    public Guid Value { get; }

    private Identifier() => Value = Guid.NewGuid();
    private Identifier(Guid value) => Value = value;

    internal static Identifier<T> CreateNew() => new();
    internal static Identifier<T> Create(Guid value) => new(value);

    public override bool Equals(object? obj) => obj is Identifier<T> other && Value == other.Value;

    public override int GetHashCode() => HashCode.Combine(Value);

    public static bool operator ==(Identifier<T> left, Identifier<T> right) =>
        left is null ? right is null : right is not null && left.Equals(right);

    public static bool operator !=(Identifier<T> left, Identifier<T> right) => !(left == right);
}
