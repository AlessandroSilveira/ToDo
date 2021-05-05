using ToDo.Domain.Auth;
using ToDo.Domain.Base;
using ToDo.Domain.Commands.AuthCommands;

namespace ToDo.Domain.Repositories.Interfaces
{
    public interface IRefreshTokenRepository : IRepository<RefreshToken>
    {
    }
}
