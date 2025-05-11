
using TodoHelper.Domain.BaseClasses;
using TodoHelper.Domain.Errors;
using TodoHelper.Domain.Results;
using TodoHelper.Domain.ValueObjects;

namespace TodoHelper.Domain.Entities;

public sealed class Category : Entity<Category>
{
    public override Identifier<Category> Id { get; }
    public IEnumerable<Todo> Todos { get; }
    public Descriptor Name { get; }

#pragma warning disable CS8618
    private Category() { }
#pragma warning restore CS8618
    private Category(Identifier<Category> id, Descriptor name, IEnumerable<Todo> todos)
    {
        Id = id;
        Name = name;
        Todos = todos;
    }

    public static Result<Category> CreateNew(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result<Category>.Failure(CategoryErrors.NameValueNotValid());
        }
        else if (name.Length > 40)
        {
            return Result<Category>.Failure(CategoryErrors.NameLengthNotValid(40));
        }
        else
        {
            Identifier<Category> id = Identifier<Category>.CreateNew();
            Descriptor nameDescriptor = new(name);
            Category category = new(id, nameDescriptor, []);
            return Result<Category>.Success(category);
        }
    }

    public static Result<Category> Create(Guid id, string name, IEnumerable<Todo> todos)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result<Category>.Failure(CategoryErrors.NameValueNotValid());
        }
        else if (name.Length > 40)
        {
            return Result<Category>.Failure(CategoryErrors.NameLengthNotValid(40));
        }
        else
        {
            Identifier<Category> idValue = Identifier<Category>.Create(id);
            Descriptor nameValue = new(name);
            Category category = new(idValue, nameValue, todos);
            return Result<Category>.Success(category);
        }
    }
}
