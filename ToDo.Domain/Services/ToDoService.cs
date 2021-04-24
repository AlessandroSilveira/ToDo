using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Domain.Entities;
using ToDo.Domain.Repositories.Interfaces;
using ToDo.Domain.Services.Interfaces;

namespace ToDo.Domain.Services
{
    public class ToDoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;

        public ToDoService(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<IEnumerable<TodoItem>> GetAll(string user)
        {
            return await _todoRepository.Search(a => a.User == user);
        }

        public async Task<IEnumerable<TodoItem>> GetAllDone(string user)
        {
            return await _todoRepository.Search(a => a.Done == true && a.User == user);
        }

        public async Task<IEnumerable<TodoItem>> GetAllUndone(string user)
        {
            return await _todoRepository.Search(a => a.Done == false && a.User == user);
        }

        public async Task<IEnumerable<TodoItem>> GetByPeriod(string user, DateTime nowDate, bool done)
        {
            return await _todoRepository.Search(a => a.Done == done && a.User == user && a.Date == nowDate);
        }
    }
}