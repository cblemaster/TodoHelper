
using TodoHelper.Domain.Results;
using TodoHelper.Domain.ValueObjects;

namespace TodoHelper.Domain.Entities;

public sealed class Todo : Entity<Todo>
{
    #region Properties
    public override Identifier<Todo> Id { get; }
    public Category Category { get; private set; } = default!;
    public Identifier<Category> CategoryId { get; private set; }
    public Description Description { get; private set; }
    public DueDate DueDate { get; private set; }
    public CompleteDate CompleteDate { get; private set; }
    public override CreateDate CreateDate { get; }
    public override UpdateDate UpdateDate { get; protected set; }
    public Importance Importance { get; private set; }
    public bool IsComplete => CompleteDate is not null && CompleteDate.Value is not null;
    public bool CanBeUpdated => !IsComplete;
    public bool CanBeDeleted => !Importance.IsImportant;
    #endregion Properties

    #region Constructors
#pragma warning disable CS8618
    private Todo() { }
#pragma warning restore CS8618
    private Todo(Identifier<Category> categoryId, Description description, DueDate dueDate, CompleteDate completeDate, CreateDate createDate, UpdateDate updateDate, Importance importance)
    {
        Id = Identifier<Todo>.CreateNew();
        CategoryId = categoryId;
        Description = description;
        DueDate = dueDate;
        CompleteDate = completeDate;
        CreateDate = createDate;
        UpdateDate = updateDate;
        Importance = importance;
    }
    #endregion Constructors

    #region Methods
    public void SetDescription(string description)
    {
        Result<Description> descriptionResult = Description.Create(description);
        if (descriptionResult.IsSuccess && descriptionResult.Value is not null)
        {
            Description = descriptionResult.Value;
            UpdateDate = UpdateDate.Create();
        }
        else
        {
            // TODO: ??????
        }
    }

    public void SetDueDate(DateOnly? dueDate)
    {
        DueDate = DueDate.Create(dueDate);
        UpdateDate = UpdateDate.Create();
    }

    public void SetImportance()
    {
        Importance = Importance.Create(!Importance.IsImportant);
        UpdateDate = UpdateDate.Create();
    }

    public void SetCategoryId(Identifier<Category> categoryId)
    {
        CategoryId = categoryId;
        UpdateDate = UpdateDate.Create();
    }

    public void SetCompleteDate(DateTimeOffset? completeDate)
    {
        CompleteDate = CompleteDate.Create(completeDate);
        UpdateDate = UpdateDate.Create();
    }
    #endregion Methods

    #region Factory
    public static Result<Todo> CreateNew(Guid categoryId, string description, DateOnly? dueDate)
    {
        Result<Description> descriptionResult = Description.Create(description);

        return descriptionResult.IsSuccess && descriptionResult.Value is Description newDescription
            ? Result<Todo>.Success(new(
                Identifier<Category>.Create(categoryId),
                newDescription,
                DueDate.Create(dueDate),
                CompleteDate.CreateNew(),
                CreateDate.CreateNew(),
                UpdateDate.CreateNew(),
                Importance.CreateNew()
                ))
            : descriptionResult.IsFailure && descriptionResult.Error is string error
                ? Result<Todo>.Failure(error)
                : Result<Todo>.Failure("An unknown error occurred while creating todo.");
    }
    #endregion Factory
}
