
using TodoHelper.Domain.ValueObjects;

namespace TodoHelper.Domain.Entities;

internal abstract class Entity<T>
{
    internal abstract Identifier<T> Id { get; }
}
