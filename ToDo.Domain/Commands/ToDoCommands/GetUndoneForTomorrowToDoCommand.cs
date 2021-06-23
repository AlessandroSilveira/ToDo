using System.Collections.Generic;
using MediatR;
using ToDo.Domain.Entities;

namespace ToDo.Domain.Commands.ToDoCommands
{

    public class GetUndoneForTomorrowToDoCommand : IRequest<IEnumerable<TodoItem>>
    {
        public GetUndoneForTomorrowToDoCommand()
        {
        }
        public GetUndoneForTomorrowToDoCommand(string user)
        {
            User = user;
        }

        public string User { get; set; }
    }
}
