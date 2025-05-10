
using TodoHelper.Domain.BaseClasses;
using TodoHelper.Domain.Results;
using TodoHelper.Domain.ValueObjects;

namespace TodoHelper.Domain.Entities;

public sealed class Todo : Entity<Todo>
{
    #region Properties
    public override Identifier<Todo> Id { get; }
    public Category Category { get; } = default!;
    public Identifier<Category> CategoryId { get; }
    public Descriptor Description { get; }
    public DueDate? DueDate { get; }
    public CompleteDate? CompleteDate { get; }
    public Importance Importance { get; }    
    #endregion Properties

    #region Constructors
#pragma warning disable CS8618
    private Todo() { }
#pragma warning restore CS8618
    private Todo(Identifier<Todo> id, Identifier<Category> categoryId, Descriptor description, DueDate? dueDate, CompleteDate? completeDate, Importance importance)
    {
        Id = id;
        CategoryId = categoryId;
        Description = description;
        DueDate = dueDate;
        CompleteDate = completeDate;
        Importance = importance;
    }
    #endregion Constructors

    #region Factory
    public static Result<Todo> CreateNew(Guid categoryId, string description, DateOnly? dueDate)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            return Result<Todo>.ValidationFailure("Todo description is required and cannot consist of exclusively whitespace characters.");
        }
        else if (description.Length > 40)
        {
            return Result<Todo>.ValidationFailure("Todo description cannot exceed 40 characters.");
        }
        else
        {
            Identifier<Todo> id = Identifier<Todo>.CreateNew();
            Identifier<Category> categoryIdValue = Identifier<Category>.Create(categoryId);
            Descriptor descriptionValue = new(description);
            DueDate? dueDateValue = new(dueDate);
            CompleteDate? completeDateValue = null;
            Importance importanceValue = new(false);
            Todo todo = new(id, categoryIdValue, descriptionValue, dueDateValue, completeDateValue, importanceValue);
            return Result<Todo>.Success(todo);
        }
    }

    public static Result<Todo> Create(Guid id, Guid categoryId, string description, DateOnly? dueDate, DateTimeOffset? completeDate, bool isImportant)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            return Result<Todo>.ValidationFailure("Todo description is required and cannot consist of exclusively whitespace characters.");
        }
        else if (description.Length > 40)
        {
            return Result<Todo>.ValidationFailure("Todo description cannot exceed 40 characters.");
        }
        else
        {
            Identifier<Todo> idValue = Identifier<Todo>.Create(id);
            Identifier<Category> categoryIdValue = Identifier<Category>.Create(categoryId);
            Descriptor descriptionValue = new(description);
            DueDate? dueDateValue = new(dueDate);
            CompleteDate? completeDateValue = new(completeDate);
            Importance importanceValue = new(isImportant);
            Todo todo = new(idValue, categoryIdValue, descriptionValue, dueDateValue, completeDateValue, importanceValue);
            return Result<Todo>.Success(todo);
        }
    }
    #endregion Factory
}
