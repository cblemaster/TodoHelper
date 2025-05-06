
using TodoHelper.Domain.Results;
using TodoHelper.Domain.ValueObjects;

namespace TodoHelper.Domain.Entities;

public sealed class Category : Entity<Category>
{
    public override Identifier<Category> Id { get; }
    public IEnumerable<Todo> Todos { get; private set; } = [];
    public Name Name { get; private set; }
    public override CreateDate CreateDate { get; }
    public override UpdateDate UpdateDate { get; protected set; }

#pragma warning disable CS8618
    private Category() { }
#pragma warning restore CS8618
    private Category(Name name, CreateDate createDate, UpdateDate updateDate)
    {
        Id = Identifier<Category>.CreateNew();
        Name = name;
        CreateDate = createDate;
        UpdateDate = updateDate;
    }

    public Result<Category> SetName(string name)
    {
        Result<Name> nameResult = Name.Create(name);
        if (nameResult.IsSuccess && nameResult.Value is Name newName)
        {
            Name = newName;
            UpdateDate = UpdateDate.Create();
            return Result<Category>.Success(this);
        }
        else
        {
            return nameResult.IsFailure && nameResult.Error is string error
                ? Result<Category>.ValidationFailure(error)
                : Result<Category>.UnknownFailure(DomainErrors.UnknownErrorMessage("updating the category"));
        }
    }

    public static Result<Category> CreateNew(string name)
    {
        Result<Name> nameResult = Name.Create(name);

        return nameResult.IsSuccess && nameResult.Value is Name newName
            ? Result<Category>.Success(new(newName, CreateDate.CreateNew(), UpdateDate.CreateNew()))
            : nameResult.IsFailure && nameResult.Error is string error
                ? Result<Category>.ValidationFailure(error)
                : Result<Category>.ValidationFailure(DomainErrors.UnknownErrorMessage("creating category"));
    }
}
