using System.Collections.Generic;
using MediatR;
using ToDo.Domain.Entities;

namespace ToDo.Domain.Commands.ToDoCommands
{
    public class GetUndoneForTodayToDoCommand : IRequest<IEnumerable<TodoItem>>
    {
        public GetUndoneForTodayToDoCommand()
        {

        }
        public GetUndoneForTodayToDoCommand(string user)
        {
            User = user;
        }

        public string User { get; set; }
    }
}
