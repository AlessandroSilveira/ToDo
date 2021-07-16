using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ToDo.Domain.Auth;
using ToDo.Domain.Commands.AuthCommands;
using ToDo.Domain.Repositories.Interfaces;

namespace ToDo.Domain.Handlers.AuthHandlers
{
    public class UpdateRefreshTokenCommandHandler : IRequestHandler<UpdateRefreskTokenCommand, RefreshToken>
    {   public UpdateRefreshTokenCommandHandler()
        {
            
        }

        public async Task<RefreshToken> Handle(UpdateRefreskTokenCommand request, CancellationToken cancellationToken)
        {   return new();
           
        }
    }
}
