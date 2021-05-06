using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using ToDo.Domain.Commands.UserCommand;
using ToDo.Domain.Entities;
using ToDo.Domain.Repositories.Interfaces;

namespace ToDo.Domain.Handlers.UserHandlers
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, User>
    {
        private readonly IUserRepository _userRepository;

        public AddUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<User> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            return _userRepository.Add(request.User);
        }
    }
}
