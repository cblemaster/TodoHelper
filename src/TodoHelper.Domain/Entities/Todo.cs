
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
    public static Result<Todo> CreateNew(Guid categoryId, string description, DateOnly? dueDate)
    {
        Descriptor descriptionDescriptor = new(Value: description, DataDefinitions.TODO_DESCRIPTION_MAX_LENGTH, DataDefinitions.TODO_DESCRIPTION_ATTRIBUTE);
        Result<Descriptor> result = descriptionDescriptor.Validate();

        if (result.IsFailure)
        {
            return Result<Todo>.Failure(DescriptorErrors.NotValid(result.Error.Description));
        }
        else
        {
            Identifier<Todo> id = Identifier<Todo>.CreateNew();
            Identifier<Category> categoryIdValue = Identifier<Category>.Create(categoryId);
            DueDate? dueDateValue = new(dueDate);
            CompleteDate? completeDateValue = null;
            Importance importanceValue = new(false);
            Todo todo = new(id, categoryIdValue, descriptionDescriptor, dueDateValue, completeDateValue, importanceValue);
            return Result<Todo>.Success(todo);
        }
    }

    public static Result<Todo> Create(Guid id, Guid categoryId, string description, DateOnly? dueDate, DateTimeOffset? completeDate, bool isImportant)
    {
        Descriptor descriptionDescriptor = new(Value: description, DataDefinitions.TODO_DESCRIPTION_MAX_LENGTH, DataDefinitions.TODO_DESCRIPTION_ATTRIBUTE);
        Result<Descriptor> result = descriptionDescriptor.Validate();

        if (result.IsFailure)
        {
            return Result<Todo>.Failure(DescriptorErrors.NotValid(result.Error.Description));
        }
        else
        {
            Identifier<Todo> idValue = Identifier<Todo>.Create(id);
            Identifier<Category> categoryIdValue = Identifier<Category>.Create(categoryId);
            DueDate? dueDateValue = new(dueDate);
            CompleteDate? completeDateValue = new(completeDate);
            Importance importanceValue = new(isImportant);
            Todo todo = new(idValue, categoryIdValue, descriptionDescriptor, dueDateValue, completeDateValue, importanceValue);
            return Result<Todo>.Success(todo);
        }
    }
    #endregion Factory
}
