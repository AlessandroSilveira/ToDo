using System;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ToDo.Domain.Commands.UserCommand;
using ToDo.Domain.Entities;
using ToDo.Domain.Repositories.Interfaces;

namespace ToDo.Domain.Handlers.UserHandlers
{
    public class GetUserByIdCommandHandler : IRequestHandler<GetUserByIdCommand, User>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Handle(GetUserByIdCommand request, CancellationToken cancellationToken)
        {
            var dados = await _userRepository.Search(a =>a.Id == Guid.Parse(request.UserId));
            return await Task.FromResult(dados.FirstOrDefault());
        }
    }
}
