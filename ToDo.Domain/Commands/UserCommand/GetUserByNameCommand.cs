using MediatR;
using ToDo.Domain.Entities;

namespace ToDo.Domain.Commands.UserCommand
{
    public class GetUserByNameCommand : IRequest<User>
    {
        public GetUserByNameCommand(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}