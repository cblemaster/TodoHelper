
using TodoHelper.Domain.Results;
using TodoHelper.Domain.ValueObjects;

namespace TodoHelper.Domain.Entities;

internal sealed class Category : Entity<Category>
{
    internal override Identifier<Category> Id { get; }
    internal IEnumerable<Todo> Todos { get; }
    internal Name Name { get; private set; }
    internal bool CanBeDeleted => !Todos.Any();

    private Category(IEnumerable<Todo> todos, Name name)
    {
        Id = Identifier<Category>.CreateNew();
        Todos = todos;
        Name = name;
    }

    internal void Rename(string name)
    {
        Result<Name> nameResult = Name.CreateNew(name);
        if (nameResult.IsSuccess && nameResult.Value is not null)
        {
            Name = nameResult.Value;
        }
    }

    internal static Result<Category> CreateNew(string name)
    {
        Result<Name> nameResult = Name.CreateNew(name);

        return nameResult.IsSuccess && nameResult.Value is not null
            ? Result<Category>.Success(new([], nameResult.Value))
            : nameResult.IsFailure && nameResult.Error is not null
                ? Result<Category>.Failure(nameResult.Error)
                : Result<Category>.Failure("An unknown error occurred while creating a new category.");
    }
}
