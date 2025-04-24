
using TodoHelper.Domain.Results;
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

    internal static Category CreateNew(IEnumerable<Todo> todos, string name)
    {
        Result<Name> nameResult = Name.CreateNew(name);

        return nameResult.IsSuccess && nameResult.Value is not null
            ? new(todos, nameResult.Value)
            : nameResult.IsFailure && nameResult.Error is not null
                ? InvalidCategoryName(nameResult.Error)
                : InvalidCategoryName("An unknown error occurred while creating a new category.");
    }

    internal static Category InvalidCategoryName(string error) => CreateNew([], error);
}
