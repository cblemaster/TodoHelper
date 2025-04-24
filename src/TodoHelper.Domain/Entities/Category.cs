
using TodoHelper.Domain.ValueObjects;

namespace TodoHelper.Domain.Entities;

internal sealed class Category : Entity<Category>
{
    internal override Identifier<Category> Id { get; }
    internal IEnumerable<Todo> Todos { get; }
    internal Name Name { get; }
    internal bool CanBeDeleted => !Todos.Any();

    private Category(IEnumerable<Todo> todos, Name name)
    {
        Id = Identifier<Category>.CreateNew();
        Todos = todos;
        Name = name;
    }

    internal static Category CreateNew(IEnumerable<Todo> todos, Name name) => new(todos, name);
}
