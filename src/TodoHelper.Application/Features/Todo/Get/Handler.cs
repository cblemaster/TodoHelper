﻿
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Application.Extensions;
using TodoHelper.Application.Features.Common;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.BaseClasses;
using TodoHelper.Domain.Errors;
using TodoHelper.Domain.Results;
using _Todo = TodoHelper.Domain.Entities.Todo;
namespace TodoHelper.Application.Features.Todo.Get;

internal sealed class Handler(IRepository<_Todo> repository) : HandlerBase<_Todo, Command, Response>(repository)
{
    public override async Task<Response> HandleAsync(Command command)
    {
        _Todo? entity = await _repository.GetByIdAsync(Identifier<_Todo>.Create(command.Id));
        return entity is null
            ? new Response(Result<TodoDTO>.Failure(Error.NotFound(nameof(_Todo))))
            : new Response(Result<TodoDTO>.Success(entity.MapToDTO()));
    }
}
