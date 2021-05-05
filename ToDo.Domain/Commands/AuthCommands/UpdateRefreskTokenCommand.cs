using MediatR;
using ToDo.Domain.Auth;

namespace ToDo.Domain.Commands.AuthCommands
{
    public class UpdateRefreskTokenCommand : IRequest<RefreshToken>
    {
        public UpdateRefreskTokenCommand()
        {

        }

        public UpdateRefreskTokenCommand(RefreshToken refreshToken)
        {
            RefreshToken = refreshToken;
        }

        public RefreshToken RefreshToken { get; set; }
    }
}
