
using TodoHelper.Domain.ValueObjects;

namespace TodoHelper.Domain.Entities;

internal sealed class Todo : Entity<Todo>
{
    internal override Identifier<Todo> Id { get; }
    internal Category Category { get; }
    internal Identifier<Category> CategoryId { get; }
}
