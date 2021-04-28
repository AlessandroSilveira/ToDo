using System.Collections.Generic;
using MediatR;
using ToDo.Domain.Entities;

namespace ToDo.Domain.Commands
{
    public class GetAllToDoCommand : IRequest<IEnumerable<TodoItem>>
    {
        public GetAllToDoCommand()
        {
        }
        public GetAllToDoCommand(string user)
        {
            User = user;
        }

        public string User { get; set; }
    }
}