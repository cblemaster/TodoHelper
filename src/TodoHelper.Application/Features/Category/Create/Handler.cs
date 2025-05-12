
using TodoHelper.Application.Extensions;
using TodoHelper.Application.Interfaces;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Definitions;
using TodoHelper.Domain.Errors;
using TodoHelper.Domain.Extensions;
using TodoHelper.Domain.Results;
using TodoHelper.Domain.ValueObjects;
using _Category = TodoHelper.Domain.Entities.Category;

namespace TodoHelper.Application.Features.Category.Create;

internal class Handler(ITodosRepository<_Category> repository) : ICommandHandler<Command, Response>
{
    private readonly ITodosRepository<_Category> _repository = repository;

    public async Task<Result<Response>> HandleAsync(Command command, CancellationToken cancellationToken = default)
    {
        Descriptor nameDescriptor = new(command.Name, DataDefinitions.CATEGORY_NAME_MAX_LENGTH, DataDefinitions.CATEGORY_NAME_ATTRIBUTE);
        Result<Descriptor> descriptorResult = nameDescriptor.Validate();

        if (descriptorResult.IsFailure)
        {
            return Result<Response>.Failure(DescriptorErrors.NotValid(descriptorResult.Error.Description));
        }
        if ((await _repository.GetAllAsync()).Select(c => c.Name.Value).ToHashSet().Contains(command.Name))
        {
            return Result<Response>.Failure(DescriptorErrors.NotValid($"A category named {command.Name} already exists."));
        }

        Result<_Category> result = _Category.CreateNew(command.Name);
        if (result.IsFailure)
        {
            return Result<Response>.Failure(DescriptorErrors.NotValid(result.Error.Description));
        }
        else if (result.IsSuccess && result.Value is not null and _Category category)
        {
            await _repository.CreateAsync(category);
            return Result<Response>.Success(new Response(category.MapToDTO()));
        }
        else
        {
            return Result<Response>.Failure(Error.Unknown);
        }
    }
}
