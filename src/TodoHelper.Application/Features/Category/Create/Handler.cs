
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Application.Extensions;
using TodoHelper.Application.Features.Common;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.Errors;
using TodoHelper.Domain.Results;
using _Category = TodoHelper.Domain.Entities.Category;

namespace TodoHelper.Application.Features.Category.Create;

internal sealed class Handler(IRepository<_Category> repository) : HandlerBase<_Category, Command, Response>(repository)
{
    public override async Task<Response> HandleAsync(Command command)
    {
        Result<_Category> result = _Category.CreateNew(command.Name);

        if (result.IsFailure && result.Error is Error error)
        {
            return new Response(Result<CategoryDTO>.Failure(Error.NotValid(error.Description)));
        }
        else if ((await _repository.GetAllAsync()).Select(c => c.Name.Value).Contains(command.Name))
        {
            return new Response(Result<CategoryDTO>.Failure(Error.DomainRuleViolation($"Category with name {command.Name} already exists.")));
        }
        else if (result.IsSuccess && result.Payload is _Category category)
        {
            category = await _repository.CreateAsync(category);
            return new Response(Result<CategoryDTO>.Success(category.MapToDTO()));
        }
        else
        {
            return new Response(Result<CategoryDTO>.Failure(Error.Unknown));
        }
    }
}
