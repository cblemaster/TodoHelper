
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Application.Features.Common;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.BaseClasses;
using TodoHelper.Domain.Errors;
using TodoHelper.Domain.Results;
using _Category = TodoHelper.Domain.Entities.Category;

namespace TodoHelper.Application.Features.Category.Update;

internal sealed class Handler(IRepository<_Category> repository) : HandlerBase<_Category, Command, Response>(repository)
{
    public override async Task<Response> HandleAsync(Command command)
    {
        _Category? entity = await _repository.GetByIdAsync(Identifier<_Category>.Create(command.Id));
        if (entity is null)
        {
            return new Response(Result<bool>.Failure(Error.NotFound(nameof(_Category))));
        }
        else if ((await _repository.GetAllAsync()).Select(c => c.Name.StringValue).Contains(command.Name))
        {
            return new Response(Result<bool>.Failure(Error.DomainRuleViolation($"Category with name {command.Name} already exists.")));
        }
        else
        {
            Result<_Category> result = _Category.CreateWithNewName
                (
                    entity.Id,
                    command.Name,
                    entity.Todos
                );

            if (result.IsFailure && result.Error is Error error)
            {
                return new Response(Result<bool>.Failure(Error.NotValid(error.Description)));
            }
            else if (result.IsSuccess && result.Payload is _Category category)
            {
                await _repository.UpdateAsync(category);
                return new Response(Result<bool>.Success(true));
            }
            else
            {
                return new Response(Result<bool>.Failure(Error.Unknown));
            }
        }
    }
}
