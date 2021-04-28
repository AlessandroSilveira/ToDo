using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ToDo.Domain.Auth;
using ToDo.Domain.Commands.AuthCommands;
using ToDo.Domain.Repositories.Interfaces;

namespace ToDo.Domain.Handlers.AuthHandlers
{
    public class GetRefreshTokenCommandHandler : IRequestHandler<GetRefreshTokenCommand, RefreshToken>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public GetRefreshTokenCommandHandler(IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<RefreshToken> Handle(GetRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var refreshToken =  await _refreshTokenRepository.GetById(request.RefreshToken);
            return await Task.FromResult(refreshToken);
        }
    }
}
