
using TodoHelper.Domain.Results;
using TodoHelper.Domain.ValueObjects;

namespace TodoHelper.Domain.Entities;

public sealed class Todo : Entity<Todo>
{
    public override Identifier<Todo> Id { get; }
    public Category Category { get; } = default!;
    public Identifier<Category> CategoryId { get; private set; }
    public Description Description { get; private set; }
    public DueDate DueDate { get; private set; }
    public CompleteDate CompleteDate { get; private set; }
    public CreateDate CreateDate { get; }
    public UpdateDate UpdateDate { get; private set; }
    public Importance Importance { get; private set; }
    public bool IsComplete => CompleteDate.Value is not null;
    public bool CanBeUpdated => !IsComplete;
    public bool CanBeDeleted => !Importance.IsImportant;

    private Todo(Identifier<Category> categoryId, Description description, DueDate dueDate, CompleteDate closeDate, CreateDate createDate, UpdateDate updateDate, Importance importance)
    {
        Id = Identifier<Todo>.CreateNew();
        CategoryId = categoryId;
        Description = description;
        DueDate = dueDate;
        CompleteDate = closeDate;
        CreateDate = createDate;
        UpdateDate = updateDate;
        Importance = importance;
    }

    internal void SetDescription(string description)
    {
        if (CanBeUpdated)
        {
            Result<Description> descriptionResult = Description.CreateNew(description);
            if (descriptionResult.IsSuccess && descriptionResult.Value is not null)
            {
                Description = descriptionResult.Value;
                UpdateDate = UpdateDate.CreateNew();
            }
        }
    }

    internal void SetDueDate(DateOnly? dueDate)
    {
        if (CanBeUpdated)
        {
            DueDate = DueDate.CreateNew(dueDate);
            UpdateDate = UpdateDate.CreateNew();
        }
    }

    internal void SetImportance()
    {
        if (CanBeUpdated)
        {
            Importance = Importance.CreateNew(!Importance.IsImportant);
            UpdateDate = UpdateDate.CreateNew();
        }
    }

    internal void SetCategoryId(Identifier<Category> categoryId)
    {
        if (CanBeUpdated)
        {
            CategoryId = categoryId;
            UpdateDate = UpdateDate.CreateNew();
        }
    }

    internal void SetCompleteDate(DateTimeOffset? completeDate)
    {
        CompleteDate = CompleteDate.CreateNew(completeDate);
        UpdateDate = UpdateDate.CreateNew();
    }

    internal static Result<Todo> CreateNew(Guid categoryId, string description, DateOnly? dueDate, bool isImportant)
    {
        Result<Description> descriptionResult = Description.CreateNew(description);

        return descriptionResult.IsSuccess && descriptionResult.Value is not null
            ? Result<Todo>.Success
                (
                    new
                    (
                        Identifier<Category>.Create(categoryId),
                        descriptionResult.Value,
                        DueDate.CreateNew(dueDate),
                        CompleteDate.CreateNew(null),
                        CreateDate.CreateNew(),
                        UpdateDate.CreateNew(),
                        Importance.CreateNew(isImportant)
                    )
                )
            : descriptionResult.IsFailure && descriptionResult.Error is not null
                ? Result<Todo>.Failure(descriptionResult.Error)
                : Result<Todo>.Failure("An unknown error occurred while creating a new todo.");
    }
}
