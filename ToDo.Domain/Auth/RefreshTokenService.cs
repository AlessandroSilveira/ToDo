using System;
using System.Linq;
using ToDo.Domain.Repositories.Interfaces;

namespace ToDo.Domain.Auth
{
    public static class RefreshTokenService
    {
        public static IRefreshTokenRepository _refreshTokenRepository { get; }

        public static string CreateRefreshToken(string token)
        {
            var refreshToken = new RefreshToken()
            {
                Token = RandomString(25) + Guid.NewGuid(),
                Expires = DateTime.UtcNow.AddYears(1),
                Created = DateTime.Now
            };

            _refreshTokenRepository.Add(refreshToken);

            return refreshToken.Token;
        }

        public static string RandomString(int length)
        {
            var random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
