
namespace TodoHelper.Domain.BaseClasses;

public sealed class Identifier<T>
{
    public Guid GuidValue { get; }

    private Identifier(Guid value) => GuidValue = value;

    public static Identifier<T> Create(Guid value) => new(value);
    internal static Identifier<T> CreateNew() => Create(Guid.NewGuid());    

    public override bool Equals(object? obj) => obj is Identifier<T> other && GuidValue == other.GuidValue;

    public override int GetHashCode() => HashCode.Combine(GuidValue);

    public static bool operator ==(Identifier<T> left, Identifier<T> right) =>
        left is null ? right is null : right is not null && left.Equals(right);

    public static bool operator !=(Identifier<T> left, Identifier<T> right) => !(left == right);
}
