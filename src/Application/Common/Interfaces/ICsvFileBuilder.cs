using PoshtaBy.Application.TodoLists.Queries.ExportTodos;
using System.Collections.Generic;

namespace PoshtaBy.Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
    }
}
