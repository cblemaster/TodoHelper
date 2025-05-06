
using TodoHelper.Domain.Results;
using TodoHelper.Domain.Specifications;
using TodoHelper.Domain.ValueObjects;

namespace TodoHelper.Domain.Entities;

public sealed class Category : Entity<Category>
{
    public override Identifier<Category> Id { get; }
    public IEnumerable<Todo> Todos { get; private set; } = [];
    public Descriptor Name { get; private set; }
    public override CreateDate CreateDate { get; }
    public override UpdateDate UpdateDate { get; protected set; }

#pragma warning disable CS8618
    private Category() { }
#pragma warning restore CS8618
    private Category(Descriptor name, CreateDate createDate, UpdateDate updateDate)
    {
        Id = Identifier<Category>.CreateNew();
        Name = name;
        CreateDate = createDate;
        UpdateDate = updateDate;
    }

    public Result<Category> SetName(string name)
    {
        Result<Descriptor> nameResult = Descriptor.Create(name, nameof(Name), DataConstants.CATEGORY_NAME_MAX_LENGTH);
        if (nameResult.IsSuccess && nameResult.Value is Descriptor newName)
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
        Result<Descriptor> nameResult = Descriptor.Create(name, nameof(Name), DataConstants.CATEGORY_NAME_MAX_LENGTH);

        return nameResult.IsSuccess && nameResult.Value is Descriptor newName
            ? Result<Category>.Success(new(newName, CreateDate.CreateNew(), UpdateDate.CreateNew()))
            : nameResult.IsFailure && nameResult.Error is string error
                ? Result<Category>.ValidationFailure(error)
                : Result<Category>.UnknownFailure(DomainErrors.UnknownErrorMessage("creating category"));
    }
}
