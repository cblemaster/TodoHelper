
namespace TodoHelper.Domain.BaseClasses;

public abstract class Entity<T>
{
    public abstract Identifier<T> Id { get; }
}
