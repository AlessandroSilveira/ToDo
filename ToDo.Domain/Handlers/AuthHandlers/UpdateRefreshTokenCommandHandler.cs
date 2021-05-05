using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ToDo.Domain.Auth;
using ToDo.Domain.Commands.AuthCommands;
using ToDo.Domain.Repositories.Interfaces;

namespace ToDo.Domain.Handlers.AuthHandlers
{
    public class UpdateRefreshTokenCommandHandler : IRequestHandler<UpdateRefreskTokenCommand, RefreshToken>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public UpdateRefreshTokenCommandHandler(IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<RefreshToken> Handle(UpdateRefreskTokenCommand request, CancellationToken cancellationToken)
        {
            //var refreskToken = await _refreshTokenRepository.GetById(request.RefreshToken.UserName);

            ////refreskToken.AddedDate = request.RefreshToken.AddedDate;
            ////refreskToken.ExpiryDate = request.RefreshToken.ExpiryDate;
            ////refreskToken.IsRevoked = request.RefreshToken.IsRevoked;
            ////refreskToken.IsUsed = request.RefreshToken.IsUsed;
            ////refreskToken.JwtId = request.RefreshToken.JwtId;
            ////refreskToken.Token = request.RefreshToken.Token;
            ////refreskToken.UserId = request.RefreshToken.UserId;

            //await _refreshTokenRepository.Update(refreskToken);

            return new RefreshToken();
           
        }
    }
}
