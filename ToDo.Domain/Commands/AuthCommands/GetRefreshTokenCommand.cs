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

        public GetRefreshTokenCommand(string refreshToken)
        {
            RefreshToken = refreshToken;
        }

        public string RefreshToken { get; set; }
    }
}
