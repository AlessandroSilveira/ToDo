using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ToDo.Domain.Commands.UserCommand;
using ToDo.Domain.Entities;
using ToDo.Domain.Repositories.Interfaces;

namespace ToDo.Domain.Handlers.UserHandlers
{
    public class GetUserByNameCommandHandler : IRequestHandler<GetUserByNameCommand, User>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByNameCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Handle(GetUserByNameCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Search(a => a.Username == request.Name);
            return user.FirstOrDefault();
        }
    }
}