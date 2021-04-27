using ToDo.Domain.Entities;
using ToDo.Domain.Repositories.Interfaces;
using ToDo.Infra.Base;
using ToDo.Infra.Context;

namespace ToDo.Infra.Repositories
{
    public class TodoRepository : Repository<TodoItem>, ITodoRepository
    {
        public TodoRepository(DataContext context) : base(context)
        {
        }
    }
}