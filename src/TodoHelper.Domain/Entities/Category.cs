
using TodoHelper.Domain.BaseClasses;
using TodoHelper.Domain.Definitions;
using TodoHelper.Domain.Errors;
using TodoHelper.Domain.Results;
using TodoHelper.Domain.ValueObjects;
using TodoHelper.Domain.ValueObjects.Extensions;

namespace TodoHelper.Domain.Entities;

public sealed class Category : Entity<Category>
{
    public override Identifier<Category> Id { get; }
    public Descriptor Name { get; }
    public IEnumerable<Todo> Todos { get; }     // TODO: use private, immutable collection

#pragma warning disable CS8618
    private Category() { }
#pragma warning restore CS8618

    private Category(Identifier<Category> id, Descriptor name, IEnumerable<Todo> todos)
    {
        Id = id;
        Name = name;
        Todos = todos;
    }

    private static Result<Category> Create(Identifier<Category> id, string name, IEnumerable<Todo> todos)
    {
        Descriptor nameDescriptor = new(Value: name,
            DataDefinitions.CATEGORY_NAME_MAX_LENGTH,
            DataDefinitions.CATEGORY_NAME_ATTRIBUTE,
            DataDefinitions.IS_CATEGORY_NAME_UNIQUE);

        Result<Descriptor> result = nameDescriptor.GetValidDescriptorOrValidationError();

        switch (result)
        {
            case Result<Descriptor> failure
                when failure.IsFailure &&
                    failure.Error is Error error:
                return Result<Category>.Failure(Error.NotValid(error.Description));
            case Result<Descriptor> success
                when success.IsSuccess &&
                    success.Payload is Descriptor descriptor:
                {
                    Category category = new(id, descriptor, todos);
                    return Result<Category>.Success(category);
                }
            default:
                return Result<Category>.Failure(Error.Unknown);
        }
    }

    public static Result<Category> CreateNew(string name) =>
        Create(Identifier<Category>.CreateNew(), name, []);

    public static Result<Category> CreateWithNewName(Identifier<Category> id, string name, IEnumerable<Todo> todos) =>
        Create(id, name, todos);
}
