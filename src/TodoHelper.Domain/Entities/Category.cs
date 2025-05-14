
using System.Collections.Immutable;
using TodoHelper.Domain.BaseClasses;
using TodoHelper.Domain.Definitions;
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
        Todos = [.. todos];
    }
    
    public static Result<Category> Create(Guid id, string name, IEnumerable<Todo> todos)
    {
        Descriptor nameDescriptor = new(Value: name, DataDefinitions.CATEGORY_NAME_MAX_LENGTH, DataDefinitions.CATEGORY_NAME_ATTRIBUTE);
        Result<Descriptor> result = nameDescriptor.Validate();

        if (result.IsFailure && result.Error is Error error)
        {
            return Result<Category>.Failure(Error.NotValid(error.Description));
        }
        else if (result.IsSuccess && result.Value is Descriptor descriptor)
        {
            Identifier<Category> idValue = Identifier<Category>.Create(id);
            Category category = new(idValue, descriptor, todos);
            return Result<Category>.Success(category);
        }
        else
        {
            return Result<Category>.Failure(Error.Unknown);
        }
    }

    public static Result<Category> CreateNew(string name) => Create(Guid.NewGuid(), name, []);
    
    public Result<Category> Update(string name) => Create(Id.Value, name, Todos);
}
