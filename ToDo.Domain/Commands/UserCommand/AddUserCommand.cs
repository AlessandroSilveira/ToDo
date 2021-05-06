using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Domain.Entities;

namespace ToDo.Domain.Commands.UserCommand
{
    public class AddUserCommand : IRequest<User>
    {
        public AddUserCommand()
        {

        }

        public AddUserCommand(User user)
        {
            User = user;
        }

        public User User { get; set; }
    }
}
