
using TodoHelper.Domain.BaseClasses;
using TodoHelper.Domain.Definitions;
using TodoHelper.Domain.Errors;
using TodoHelper.Domain.Extensions;
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
    public static Result<Todo> Create(Guid id, Guid categoryId, string description, DateOnly? dueDate, DateTimeOffset? completeDate, bool isImportant)
    {
        Descriptor descriptionDescriptor = new(Value: description, DataDefinitions.TODO_DESCRIPTION_MAX_LENGTH, DataDefinitions.TODO_DESCRIPTION_ATTRIBUTE);
        Result<Descriptor> result = descriptionDescriptor.Validate();

        if (result.IsFailure && result.Error is Error error)
        {
            return Result<Todo>.Failure(Error.NotValid(error.Description));
        }
        else if (result.IsSuccess && result.Value is Descriptor descriptor)
        {
            Identifier<Todo> idValue = Identifier<Todo>.Create(id);
            Identifier<Category> categoryIdValue = Identifier<Category>.Create(categoryId);
            DueDate? dueDateValue = new(dueDate);
            CompleteDate? completeDateValue = new(completeDate);
            Importance importanceValue = new(isImportant);
            Todo todo = new(idValue, categoryIdValue, descriptor, dueDateValue, completeDateValue, importanceValue);
            return Result<Todo>.Success(todo);
        }
        else
        {
            return Result<Todo>.Failure(Error.Unknown);
        }
    }

    public static Result<Todo> CreateNew(Guid categoryId, string description, DateOnly? dueDate) =>
        Create(Guid.NewGuid(), categoryId, description, dueDate, null, false);
    #endregion Factory
}
