using MediatR;
using System.Collections.Generic;
using ToDo.Domain.Entities;

namespace ToDo.Domain.Commands
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
