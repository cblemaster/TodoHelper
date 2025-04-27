
using TodoHelper.Domain.Results;
using TodoHelper.Domain.ValueObjects;

namespace TodoHelper.Domain.Entities;

internal sealed class Todo : Entity<Todo>
{
    internal override Identifier<Todo> Id { get; }
    internal Category Category { get; } = default!;
    internal Identifier<Category> CategoryId { get; private set; }
    internal Description Description { get; private set; }
    internal DueDate DueDate { get; private set; }
    internal CompleteDate CompleteDate { get; private set; }
    internal CreateDate CreateDate { get; }
    internal UpdateDate UpdateDate { get; private set; }
    internal Importance Importance { get; private set; }
    internal bool IsComplete => CompleteDate.Value is not null;
    internal bool CanBeUpdated => !IsComplete;
    internal bool CanBeDeleted => !Importance.IsImportant;

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
