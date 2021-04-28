using MediatR;
using System.Collections.Generic;
using ToDo.Domain.Entities;

namespace ToDo.Domain.Commands
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
