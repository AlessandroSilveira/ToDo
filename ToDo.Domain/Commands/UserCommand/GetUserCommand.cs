using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Domain.Entities;

namespace ToDo.Domain.Commands.UserCommand
{  

    public class GetUserCommand : IRequest<User>
    {
        public GetUserCommand()
        {

        }

        public GetUserCommand(User user)
        {
            User = user;
        }

        public User User { get; set; }
    }
}
