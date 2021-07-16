using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ToDo.Domain.Auth;
using ToDo.Domain.Commands.AuthCommands;
using ToDo.Domain.Repositories.Interfaces;

namespace ToDo.Domain.Handlers.AuthHandlers
{
    public class AddRefreshTokenCommandHandler : IRequestHandler<AddRefreshTokenCommand, RefreshToken>
    {
        private readonly IRefreshTokenCacheRepository _refreshTokenRepository;

        public AddRefreshTokenCommandHandler(IRefreshTokenCacheRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<RefreshToken> Handle(AddRefreshTokenCommand request, CancellationToken cancellationToken)
        {
             _refreshTokenRepository.Add(request.RefreshToken);
            return await Task.FromResult(request.RefreshToken);
        }
    }
}
