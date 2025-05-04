
using TodoHelper.Domain.ValueObjects;

namespace TodoHelper.Domain.Entities;

public abstract class Entity<T>
{
    public abstract Identifier<T> Id { get; }
    public abstract CreateDate CreateDate { get; }
    public abstract UpdateDate UpdateDate { get; protected set; }
}
