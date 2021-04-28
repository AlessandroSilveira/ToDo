using MediatR;
using System;
using ToDo.Domain.Auth;

namespace ToDo.Domain.Commands.AuthCommands
{
    public class GetRefreshTokenCommand : IRequest<RefreshToken>
    {
        public GetRefreshTokenCommand()
        {
        }        

        public GetRefreshTokenCommand(Guid refreshToken)
        {
            RefreshToken = refreshToken;
        }

        public Guid RefreshToken { get; set; }
    }
}
