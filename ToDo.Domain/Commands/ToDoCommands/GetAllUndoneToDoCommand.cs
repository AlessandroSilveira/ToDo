using System.Collections.Generic;
using MediatR;
using ToDo.Domain.Entities;

namespace ToDo.Domain.Commands.ToDoCommands
{
    public class GetAllUndoneToDoCommand : IRequest<IEnumerable<TodoItem>>
    {
        public GetAllUndoneToDoCommand()
        {

        }
        public GetAllUndoneToDoCommand(string user)
        {
            User = user;
        }

        public string User { get; set; }
    }
}
