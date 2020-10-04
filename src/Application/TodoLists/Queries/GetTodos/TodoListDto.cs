﻿using PoshtaBy.Application.Common.Mappings;
using PoshtaBy.Domain.Entities;
using System.Collections.Generic;

namespace PoshtaBy.Application.TodoLists.Queries.GetTodos
{
    public class TodoListDto : IMapFrom<TodoList>
{
    public TodoListDto()
    {
        Items = new List<TodoItemDto>();
    }

    public int Id { get; set; }

    public string Title { get; set; }

    public IList<TodoItemDto> Items { get; set; }
}
}
