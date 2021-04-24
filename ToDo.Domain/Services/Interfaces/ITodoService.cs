using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Domain.Entities;

namespace ToDo.Domain.Services.Interfaces
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoItem>> GetAll(string user);
        Task<IEnumerable<TodoItem>> GetAllDone(string user);
        Task<IEnumerable<TodoItem>> GetAllUndone(string user);
        Task<IEnumerable<TodoItem>> GetByPeriod(string user, DateTime nowDate, bool b);
    }
}