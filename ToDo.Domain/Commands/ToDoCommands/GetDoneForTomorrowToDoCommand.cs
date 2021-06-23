using System.Collections.Generic;
using MediatR;
using ToDo.Domain.Entities;

namespace ToDo.Domain.Commands.ToDoCommands
{
    public class GetDoneForTomorrowToDoCommand : IRequest<IEnumerable<TodoItem>>
    {
        public GetDoneForTomorrowToDoCommand()
        {
        }
        public GetDoneForTomorrowToDoCommand(string user)
        {
            User = user;
        }

        public string User { get; set; }
    }
}
