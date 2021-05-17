using System.Threading.Tasks;
using ToDo.Domain.Entities;
using Refit;
using System.Collections.Generic;

namespace ToDo.Domain.Services
{
    public interface IExampleGetToDoService
    {
        [Get("/api/ExampleGetToDo/GetToDos")]
        Task<IEnumerable<TodoItemResponse>> GetAllToDo();
    }
}