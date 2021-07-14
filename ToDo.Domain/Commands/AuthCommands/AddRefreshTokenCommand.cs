using System.Threading.Tasks;
using MediatR;
using ToDo.Domain.Auth;

namespace ToDo.Domain.Commands.AuthCommands
{
    public class AddRefreshTokenCommand : IRequest<RefreshToken>
    {
        public AddRefreshTokenCommand()
        {
        }

        public AddRefreshTokenCommand(RefreshToken refreshToken)
        {
            RefreshToken = refreshToken;
        }

        public RefreshToken RefreshToken { get; set; }
    }
}
