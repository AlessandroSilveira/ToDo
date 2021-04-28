using MediatR;
using System.Collections.Generic;
using ToDo.Domain.Entities;

namespace ToDo.Domain.Commands
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
