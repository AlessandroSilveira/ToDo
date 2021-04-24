using System;
using System.Collections.Generic;
using ToDo.Domain.Base;
using ToDo.Domain.Entities;

namespace ToDo.Domain.Repositories.Interfaces
{
    public interface ITodoRepository : IRepository<TodoItem>
    {
      
    }
}