﻿
using TodoHelper.Application.DataTransferObjects;
using TodoHelper.Domain.Results;

namespace TodoHelper.Application.Features.Todo.GetDueToday;

internal sealed record Response(Result<IEnumerable<TodoDTO>> Todos);
