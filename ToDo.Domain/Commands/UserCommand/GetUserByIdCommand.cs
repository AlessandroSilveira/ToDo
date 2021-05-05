using MediatR;
using System;
using ToDo.Domain.Entities;

namespace ToDo.Domain.Commands.UserCommand
{
    public class GetUserByIdCommand : IRequest<User>
    {
        public GetUserByIdCommand()
        {

        }

        public GetUserByIdCommand(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; set; }
    }
}
