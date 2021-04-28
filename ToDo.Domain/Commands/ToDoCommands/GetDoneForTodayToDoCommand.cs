using MediatR;
using System.Collections.Generic;
using ToDo.Domain.Entities;

namespace ToDo.Domain.Commands
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
