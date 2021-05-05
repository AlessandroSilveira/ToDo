
namespace ToDo.Domain.Services.Interfaces
{
    public interface IRefreshTokenService
    {
        string CreateRefreshToken(string token);
        string RandomString(int length);
    }
}
