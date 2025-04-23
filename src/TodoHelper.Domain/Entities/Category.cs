
using TodoHelper.Domain.ValueObjects;

namespace TodoHelper.Domain.Entities;

internal sealed class Category : Entity<Category>
{
    internal override Identifier<Category> Id { get; }
    internal IEnumerable<Todo> Todos { get; }
}
