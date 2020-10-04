using PoshtaBy.Application.Common.Mappings;
using PoshtaBy.Domain.Entities;

namespace PoshtaBy.Application.TodoLists.Queries.ExportTodos
{
    public class TodoItemRecord : IMapFrom<TodoItem>
    {
        public string Title { get; set; }

        public bool Done { get; set; }
    }
}
