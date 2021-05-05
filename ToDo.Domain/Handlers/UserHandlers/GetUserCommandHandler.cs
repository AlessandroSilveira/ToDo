using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ToDo.Domain.Commands.UserCommand;
using ToDo.Domain.Entities;
using ToDo.Domain.Repositories.Interfaces;

namespace ToDo.Domain.Handlers.UserHandlers
{
    public class GetUserCommandHandler : IRequestHandler<GetUserCommand, User>
    {
        private readonly IUserRepository _userRepository;

        public GetUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Handle(GetUserCommand request, CancellationToken cancellationToken)
        {
            var dados = await _userRepository.Search(a => a.Username == request.User.Username && a.Password == request.User.Password);
            return await Task.FromResult(dados.FirstOrDefault());
        }
    }
}
