
using System.Collections.Immutable;
using TodoHelper.Domain.BaseClasses;
using TodoHelper.Domain.Errors;
using TodoHelper.Domain.Extensions;
using TodoHelper.Domain.Results;
using TodoHelper.Domain.ValueObjects;

namespace TodoHelper.Domain.Entities;

public sealed class Category : Entity<Category>
{
    public override Identifier<Category> Id { get; }
    public ImmutableList<Todo> Todos { get; }
    public Descriptor Name { get; }

#pragma warning disable CS8618
    private Category() { }
#pragma warning restore CS8618
    private Category(Identifier<Category> id, Descriptor name, IEnumerable<Todo> todos)
    {
        Id = id;
        Name = name;
        Todos = todos.ToImmutableList<Todo>();
    }

    public static Result<Category> CreateNew(string name)
    {
        Descriptor nameDescriptor = new(Value: name, MaxLength: 40, "Category name");
        Result<Descriptor> result = nameDescriptor.Validate();

        if (result.IsFailure)
        {
            return Result<Category>.Failure(DescriptorErrors.NotValid(result.Error.Description));
        }
        else
        {
            Identifier<Category> id = Identifier<Category>.CreateNew();
            Category category = new(id, nameDescriptor, []);
            return Result<Category>.Success(category);
        }
    }

    public static Result<Category> Create(Guid id, string name, IEnumerable<Todo> todos)
    {
        Descriptor nameDescriptor = new(Value: name, MaxLength: 40, "Category name");
        Result<Descriptor> result = nameDescriptor.Validate();

        if (result.IsFailure)
        {
            return Result<Category>.Failure(DescriptorErrors.NotValid(result.Error.Description));
        }
        else
        {
            Identifier<Category> idValue = Identifier<Category>.Create(id);
            Category category = new(idValue, nameDescriptor, todos);
            return Result<Category>.Success(category);
        }
    }
}
