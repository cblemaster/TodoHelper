
using TodoHelper.Domain.Results;
using TodoHelper.Domain.ValueObjects;

namespace TodoHelper.Domain.Entities;

internal sealed class Todo : Entity<Todo>
{
    internal override Identifier<Todo> Id { get; }
    internal Category Category { get; } = default!;
    internal Identifier<Category> CategoryId { get; }
    internal Description Description { get; }
    internal DueDate DueDate { get; }
    internal CompleteDate CompleteDate { get; }
    internal CreateDate CreateDate { get; }
    internal UpdateDate UpdateDate { get; }
    internal Importance Importance { get; }
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
