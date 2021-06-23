using System.Collections.Generic;
using MediatR;
using ToDo.Domain.Entities;

namespace ToDo.Domain.Commands.ToDoCommands
{   

    public class GetDoneForTodayToDoCommand : IRequest<IEnumerable<TodoItem>>
    {
        public GetDoneForTodayToDoCommand()
        {

        }
        public GetDoneForTodayToDoCommand(string user)
        {
            User = user;
        }

        public string User { get; set; }
    }
}
