using System.Collections.Generic;
using MediatR;
using ToDo.Domain.Entities;

namespace ToDo.Domain.Commands
{
    public class GetAllDoneToDoCommand : IRequest<IEnumerable<TodoItem>>
    {
        public GetAllDoneToDoCommand()
        {
                
        }
        public GetAllDoneToDoCommand(string user)
        {
            User = user;
        }

        public string User { get; set; }
    }
}