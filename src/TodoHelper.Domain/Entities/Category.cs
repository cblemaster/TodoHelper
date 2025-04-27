
using TodoHelper.Domain.Results;
using TodoHelper.Domain.ValueObjects;

namespace TodoHelper.Domain.Entities;

public sealed class Category : Entity<Category>
{
    public override Identifier<Category> Id { get; }
    public IEnumerable<Todo> Todos { get; }
    public Name Name { get; private set; }
    public bool CanBeDeleted => !Todos.Any();

#pragma warning disable CS8618
    private Category() { }
#pragma warning restore CS8618
    private Category(IEnumerable<Todo> todos, Name name)
    {
        Id = Identifier<Category>.CreateNew();
        Todos = todos;
        Name = name;
    }

    internal void Rename(string name)
    {
        Result<Name> nameResult = Name.Create(name);
        if (nameResult.IsSuccess && nameResult.Value is not null)
        {
            Name = nameResult.Value;
        }
    }

    internal static Result<Category> CreateNew(string name)
    {
        Result<Name> nameResult = Name.Create(name);

        return nameResult.IsSuccess && nameResult.Value is not null
            ? Result<Category>.Success(new([], nameResult.Value))
            : nameResult.IsFailure && nameResult.Error is not null
                ? Result<Category>.Failure(nameResult.Error)
                : Result<Category>.Failure("An unknown error occurred while creating a new category.");
    }
}
