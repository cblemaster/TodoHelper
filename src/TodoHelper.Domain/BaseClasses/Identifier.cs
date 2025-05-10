namespace TodoHelper.Domain.BaseClasses;

public class Identifier<T>
{
    public Guid Value { get; }

    private Identifier(Guid value) => Value = value;

    internal static Identifier<T> CreateNew() => new(Guid.NewGuid());
    public static Identifier<T> Create(Guid value) => new(value);

    public override bool Equals(object? obj) => obj is Identifier<T> other && Value == other.Value;

    public override int GetHashCode() => HashCode.Combine(Value);

    public static bool operator ==(Identifier<T> left, Identifier<T> right) =>
        left is null ? right is null : right is not null && left.Equals(right);

    public static bool operator !=(Identifier<T> left, Identifier<T> right) => !(left == right);
}
