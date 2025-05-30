﻿
using TodoHelper.Application.Features.Common;
using TodoHelper.DataAccess.Repository;
using TodoHelper.Domain.BaseClasses;
using TodoHelper.Domain.Entities;
using TodoHelper.Domain.Errors;
using TodoHelper.Domain.Results;
using _Category = TodoHelper.Domain.Entities.Category;

namespace TodoHelper.Application.Features.Category.Delete;

internal sealed class Handler(IRepository<_Category> repository) : HandlerBase<_Category, Command, Response>(repository)
{
    public override async Task<Response> HandleAsync(Command command)
    {
        _Category? entity = await _repository.GetByIdAsync(Identifier<_Category>.Create(command.Id));
        if (entity is null)
        {
            return new Response(Result<bool>.Failure(Error.NotFound(nameof(_Category))));
        }
        else
        {
            await _repository.DeleteAsync(entity);
            return new Response(Result<bool>.Success(true));
        }
    }
}
